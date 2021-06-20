using Kanbersky.HC.BFF.Services.Abstract;
using Kanbersky.HC.BFF.Services.Clients.Abstract;
using Kanbersky.HC.BFF.Services.Clients.Concrete;
using Kanbersky.HC.BFF.Services.Concrete;
using Kanbersky.HC.Core.Settings.Concrete.BFF;
using Kanbersky.HC.Core.Settings.Concrete.CircuitBreaker;
using Kanbersky.HealthChecks.Extensions;
using Kanbersky.HealthChecks.Models.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Kanbersky.HC.BFF.Services.Extensions
{
    public static class BFFServiceExtensions
    {
        public static IServiceCollection RegisterServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            ApiSettings apiSettings = new ApiSettings();
            configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
            services.AddSingleton(apiSettings);

            CircuitBreakerSettings circuitBreakerSettings = new CircuitBreakerSettings();
            configuration.GetSection(nameof(CircuitBreakerSettings)).Bind(circuitBreakerSettings);
            services.AddSingleton(circuitBreakerSettings);

            services.AddHttpClient<IOrderingClientService, OrderingClientService>("Ordering", c =>
                c.BaseAddress = new Uri(apiSettings.OrderingUrl))
                .AddPolicyHandler(GetRetryPolicy(circuitBreakerSettings))
                .AddPolicyHandler(GetCircuitBreakerPolicy(circuitBreakerSettings));

            services.AddHttpClient<IProductClientService, ProductClientService>("Catalog", c =>
               c.BaseAddress = new Uri(apiSettings.CatalogUrl))
               .AddPolicyHandler(GetRetryPolicy(circuitBreakerSettings))
               .AddPolicyHandler(GetCircuitBreakerPolicy(circuitBreakerSettings));

            services.AddTransient<IOrderService, OrderService>();
            services.AddScoped<IProductClientService, ProductClientService>();
            services.AddScoped<IOrderingClientService, OrderingClientService>();
            PrepareUrisGroup(services, apiSettings);

            return services;
        }

        private static void PrepareUrisGroup(IServiceCollection services, ApiSettings apiSettings)
        {
            var uris = new List<UrlGroupHealthChecksModel>
            {
                new UrlGroupHealthChecksModel
                {
                    ApiUrl = $"{apiSettings.CatalogUrl}/swagger/index.html",
                    FailureStatus = HealthStatus.Degraded,
                    Name = "Catalog Api Health"
                },
                new UrlGroupHealthChecksModel
                {
                    ApiUrl = $"{apiSettings.BasketUrl}/swagger/index.html",
                    FailureStatus = HealthStatus.Degraded,
                    Name = "Basket Api Health"
                },
                new UrlGroupHealthChecksModel
                {
                    ApiUrl = $"{apiSettings.OrderingUrl}/swagger/index.html",
                    FailureStatus = HealthStatus.Degraded,
                    Name = "Ordering Api Health"
                }
            };

            services.AddUrlGroupsHealthCheck(uris);
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(CircuitBreakerSettings settings)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: settings.RetryCount,
                    sleepDurationProvider: r => TimeSpan.FromSeconds(Math.Pow(2, r)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception} ");
                    }
                );
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(CircuitBreakerSettings settings)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: settings.HandledEventsAllowedBeforeBreaking,
                    durationOfBreak: TimeSpan.FromSeconds(settings.DurationOfBreak)
                );
        }
    }
}
