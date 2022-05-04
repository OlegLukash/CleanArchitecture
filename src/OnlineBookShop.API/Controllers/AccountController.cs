using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineBookShop.API.Requests;
using OnlineBookShop.Domain.Auth;
using OnlineBookShop.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineBookShop.API.Controllers
{
    [Route("api/account")]
    public class AccountController : AppBaseController
    {
        private readonly AuthOptions _authenticationOptions;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IOptions<AuthOptions> authenticationOptions, SignInManager<User> signInManager)
        {
            _authenticationOptions = authenticationOptions.Value;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userForLoginDto.Username, userForLoginDto.Password, false, false);

            if (checkingPasswordResult.Succeeded)
            {
                var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authenticationOptions.Issuer,
                     audience: _authenticationOptions.Audience,
                     claims: new List<Claim>(),
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(new { AccessToken = encodedToken });
            }

            return Unauthorized();
        }


    }
}
