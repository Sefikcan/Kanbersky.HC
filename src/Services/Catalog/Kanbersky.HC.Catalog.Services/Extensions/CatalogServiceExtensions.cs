using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.HC.Catalog.Services.Extensions
{
    public static class CatalogServiceExtensions
    {
        public static IServiceCollection RegisterServiceLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
