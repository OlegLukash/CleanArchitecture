using Microsoft.AspNetCore.Identity;
using OnlineBookShop.Domain.Auth;
using OnlineBookShop.Infrastructure.Persistance.Contexts;
using OnlineBookShop.Infrastructure.Persistance.DataSeed;

namespace OnlineBookShop.API.Extensions
{
    public static class HostExtensions
    {
        public static async Task SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OnlineBookShopDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();

                    await SeedFacade.SeedData(context, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
        }
    }
}
