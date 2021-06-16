using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.HC.Basket.Services.Extensions
{
    public static class BasketServiceExtensions
    {
        public static IServiceCollection RegisterBasketServiceLayer(this IServiceCollection services)
        {
            var c = Assembly.GetExecutingAssembly();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
