using Microsoft.AspNetCore.Identity;
using OnlineBookShop.Domain.Auth;

namespace OnlineBookShop.Infrastructure.Persistance.DataSeed
{
    public class UsersSeed
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    UserName = "admin",
                    Email = "admin@onlinebookshop.com",
                };

                await userManager.CreateAsync(user, "Qwerty1!");
            }
        }
    }
}
