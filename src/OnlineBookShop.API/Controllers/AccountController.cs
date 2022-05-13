using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Application.App.Auth.Commands;

namespace OnlineBookShop.API.Controllers
{
    [Route("api/account")]
    public class AccountController : AppBaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {     
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInCommand passwordSignInCommand)
        {
            var response = await _mediator.Send(passwordSignInCommand);

            if (response.Succeeded)
            {
                return Ok(new { response.AccessToken });
            }

            return Unauthorized();
        }
    }
}
