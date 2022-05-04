using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookShop.Application.Repositories;
using OnlineBookShop.Domain.Auth;
using OnlineBookShop.Infrastructure.Persistance.Contexts;
using OnlineBookShop.Infrastructure.Persistance.Repositories;

namespace OnlineBookShop.Infrastructure.Persistance.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnlineBookShopDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("OnlineBookShopConnection"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<OnlineBookShopDbContext>();

            services.AddScoped<IRepository, EFCoreRepository>();

            return services;
        }
    }
    
}
