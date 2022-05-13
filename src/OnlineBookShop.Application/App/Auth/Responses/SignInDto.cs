namespace OnlineBookShop.Application.App.Auth.Responses
{
    public class SignInDto
    {
        public bool Succeeded { get; set; }

        public string? AccessToken { get; set; }
    }
}
