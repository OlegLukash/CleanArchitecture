namespace OnlineBookShop.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> PasswordSignInAsync(string userName, string password);
    }
}
