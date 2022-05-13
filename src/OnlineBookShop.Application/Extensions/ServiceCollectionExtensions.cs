using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookShop.Application.Common.Behaviours;
using System.Reflection;

namespace OnlineBookShop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
