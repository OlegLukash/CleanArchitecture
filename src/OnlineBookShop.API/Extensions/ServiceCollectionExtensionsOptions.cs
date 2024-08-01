using OnlineBookShop.Infrastructure.Persistance.Options;

namespace OnlineBookShop.API.Extensions
{
    public static class ServiceCollectionExtensionsOptions
    {
        public static IServiceCollection AddApiOptions(this IServiceCollection services)
        {
            services.AddPersistanceOptions();
            return services;
        }

        public static IServiceCollection AddPersistanceOptions(this IServiceCollection services)
        {
            services.AddOptions<OnlineBookShopDbContextOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    settings.ConnectionString = configuration.GetValue<string>("ConnectionStrings:OnlineBookShopConnection");
                });
            return services;
        }

        public static IServiceCollection AddOption<T>(this IServiceCollection services, string section) where T : class
        {
            services.AddOptions<T>()
               .Configure<IConfiguration>((options, configure) =>
               {
                   configure.GetSection(section).Bind(options);
               });
            return services;
        }
    }
}
