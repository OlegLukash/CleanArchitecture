using MediatR;
using OnlineBookShop.Application.App.Auth.Responses;
using OnlineBookShop.Application.Common.Interfaces;

namespace OnlineBookShop.Application.App.Auth.Commands
{
    public class SignInCommand: IRequest<SignInDto>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInDto>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(IAuthenticationService authenticationService, ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        public async Task<SignInDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var passwordSignInResult = await _authenticationService.PasswordSignInAsync(request.UserName, request.Password);
            string? accessToken = null;
            if (passwordSignInResult)
            {
                accessToken = _tokenService.GenerateAccessToken();
            }

            return new SignInDto()
            { 
                Succeeded = passwordSignInResult,
                AccessToken = accessToken
            };
        }
    }
}
