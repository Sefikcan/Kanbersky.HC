using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.HC.Basket.Services.Extensions
{
    public static class BasketServiceExtensions
    {
        public static IServiceCollection RegisterServiceLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
