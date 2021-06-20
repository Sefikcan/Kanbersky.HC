using Kanbersky.HC.Core.Settings.Concrete.Databases;
using Kanbersky.HC.Ordering.Infrastructure.DataAccess.EntityFramework;
using Kanbersky.HealthChecks.Extensions;
using Kanbersky.HealthChecks.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Kanbersky.HC.Ordering.Infrastructure.Extensions
{
    public static class OrderingInfraExtensions
    {
        public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            OrderDbSettings orderDbSettings = new OrderDbSettings();
            configuration.GetSection(nameof(OrderDbSettings)).Bind(orderDbSettings);
            services.AddSingleton(orderDbSettings);

            services.AddDbContext<OrderDbContext>(c =>
                c.UseSqlServer(orderDbSettings.ConnectionStrings), ServiceLifetime.Transient);

            services.AddEntityFrameworkHealthCheck<OrderDbContext>(new EntityFrameworkHealthChecksModel 
            {
                FailureStatus = HealthStatus.Degraded,
                Name = "EntityFramework Health Status"
            });

            return services;
        }
    }
}
