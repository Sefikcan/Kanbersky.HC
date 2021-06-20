using Kanbersky.HC.Core.Settings.Concrete.Caching;
using Kanbersky.HC.Core.Settings.Concrete.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Kanbersky.HC.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            CacheSettings cacheSettings = new CacheSettings();
            configuration.GetSection(nameof(CacheSettings)).Bind(cacheSettings);
            services.AddSingleton(cacheSettings);

            services.AddHealthChecks()
                        .AddRedis(cacheSettings.ConnectionString, "Redis Health", HealthStatus.Degraded);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                        .AddMongoDb(configuration["MongoDBSettings:ConnectionStrings"], "MongoDB Health", HealthStatus.Degraded);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFrameworkHealthCheck<T>(this IServiceCollection services)
            where T: DbContext
        {
            services.AddHealthChecks()
                        .AddDbContextCheck<T>("EntityFramework Health", HealthStatus.Degraded);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPostgreSQLHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                        .AddNpgSql("", name: "PostgreSQL Health", failureStatus: HealthStatus.Degraded);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySQLHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                        .AddMySql("", name: "MySQL Health", failureStatus: HealthStatus.Degraded);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMsSQLHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                        .AddSqlServer("", name: "MsSQL Health", failureStatus: HealthStatus.Degraded);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddElasticsearchHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            ElasticsearchSettings elasticsearchSettings = new ElasticsearchSettings();
            configuration.GetSection(nameof(ElasticsearchSettings)).Bind(elasticsearchSettings);
            services.AddSingleton(elasticsearchSettings);

            services.AddHealthChecks()
                        .AddElasticsearch(elasticsearchSettings.Url, name: "Elasticsearch Health", failureStatus: HealthStatus.Degraded);

            return services;
        }
    }
}
