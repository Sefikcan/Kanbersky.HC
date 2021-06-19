using Kanbersky.HC.BFF.Services.Abstract;
using Kanbersky.HC.BFF.Services.Clients.Abstract;
using Kanbersky.HC.BFF.Services.Clients.Concrete;
using Kanbersky.HC.BFF.Services.Concrete;
using Kanbersky.HC.Core.Settings.Concrete.BFF;
using Kanbersky.HC.Core.Settings.Concrete.CircuitBreaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
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


            return services;
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
