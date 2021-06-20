using Kanbersky.HC.Core.Caching.Abstract;
using Kanbersky.HC.Core.Caching.Concrete.Redis;
using Kanbersky.HC.Core.Settings.Concrete.Caching;
using Kanbersky.HealthChecks.Extensions;
using Kanbersky.HealthChecks.Models.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.HC.Basket.Infrastructure.Extensions
{
    public static class BasketInfraExtensions
    {
        public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            CacheSettings cacheSettings = new CacheSettings();
            configuration.GetSection(nameof(CacheSettings)).Bind(cacheSettings);
            services.AddSingleton(cacheSettings);

            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettings.ConnectionString;
            });

            services.AddRedisHealthCheck(new RedisHealthChecksModel 
            {
                FailureStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded,
                Name = "Redis Health",
                RedisConnectionString = cacheSettings.ConnectionString
            });

            return services;
        }
    }
}
