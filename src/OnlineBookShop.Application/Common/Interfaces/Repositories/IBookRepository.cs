using OnlineBookShop.Application.App.Books.Dtos;

namespace OnlineBookShop.Application.Common.Interfaces.Repositories
{
    public interface IBookRepository : IRepository
    {
        Task<IEnumerable<BookDto>> GetTopRatedBooks();
    }
}
