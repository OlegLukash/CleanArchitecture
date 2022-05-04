using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Domain.Auth;
using OnlineBookShop.Infrastructure.Persistance.Contexts;

namespace OnlineBookShop.Infrastructure.Persistance.DataSeed
{
    public class SeedFacade
    {
        public static async Task SeedData(OnlineBookShopDbContext onlineBookShopDbContext, UserManager<User> userManager)
        {
            onlineBookShopDbContext.Database.Migrate();

            await PublishersSeed.Seed(onlineBookShopDbContext);
            await BooksSeed.Seed(onlineBookShopDbContext);
            await UsersSeed.Seed(userManager);
        }
    }
}
