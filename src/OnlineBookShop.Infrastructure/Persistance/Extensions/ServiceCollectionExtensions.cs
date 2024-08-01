using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineBookShop.Application.Common.Interfaces;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Domain.Auth;
using OnlineBookShop.Infrastructure.Identity;
using OnlineBookShop.Infrastructure.Persistance.Contexts;
using OnlineBookShop.Infrastructure.Persistance.Database;
using OnlineBookShop.Infrastructure.Persistance.Options;
using OnlineBookShop.Infrastructure.Persistance.Repositories;

namespace OnlineBookShop.Infrastructure.Persistance.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<OnlineBookShopDbContext>((serviceProvider, optionBuilder) =>
            {
                var dbContextOptions = serviceProvider.GetRequiredService<IOptions<OnlineBookShopDbContextOptions>>();

                optionBuilder.UseSqlServer(dbContextOptions.Value.ConnectionString);
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<OnlineBookShopDbContext>();

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetService<OnlineBookShopDbContext>());
            services.AddScoped<IRepository, EFCoreRepository>();
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();//if DI is responsible for creating disposable object, he will be responsbile for disposing it.
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
    
}
