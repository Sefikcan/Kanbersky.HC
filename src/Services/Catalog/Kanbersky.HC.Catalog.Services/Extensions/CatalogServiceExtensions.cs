using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.HC.Catalog.Services.Extensions
{
    public static class CatalogServiceExtensions
    {
        public static IServiceCollection RegisterCatalogServiceLayer(this IServiceCollection services)
        {
            var c = Assembly.GetExecutingAssembly();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
