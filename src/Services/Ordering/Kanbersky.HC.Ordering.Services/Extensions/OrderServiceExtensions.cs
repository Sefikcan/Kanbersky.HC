using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.HC.Ordering.Services.Extensions
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection RegisterOrderServiceLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
