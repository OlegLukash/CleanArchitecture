using Microsoft.AspNetCore.Identity;
using OnlineBookShop.Application.Common.Interfaces;
using OnlineBookShop.Domain.Auth;

namespace OnlineBookShop.Infrastructure.Identity
{
   
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> PasswordSignInAsync(string userName, string password)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            return checkingPasswordResult.Succeeded;
        }
    }
}
