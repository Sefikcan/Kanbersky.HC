using Kanbersky.HC.Core.Settings.Concrete.Databases;
using Kanbersky.HC.Ordering.Infrastructure.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
