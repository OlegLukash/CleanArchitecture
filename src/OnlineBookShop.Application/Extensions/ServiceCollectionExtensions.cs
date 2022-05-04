using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnlineBookShop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);

            return services;
        }
    }
}
