using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Infrastructure.Persistance.Contexts;

namespace OnlineBookShop.Infrastructure.Persistance.Repositories
{
    public class BookRepository : EFCoreRepository, IBookRepository
    {
        public BookRepository(OnlineBookShopDbContext onlineBookShopDbContext, IMapper mapper): 
            base(onlineBookShopDbContext, mapper)
        {
            
        }

        public async Task<IEnumerable<BookDto>> GetTopRatedBooks()
        {
            var result = await _onlineBookShopDbContext.Books
                .Where(book => book.Reviews.Any(r => r.NumStars > 4))
                .Select(book => new BookDto
                {
                    Id = book.Id,
                    Title = book.Title, 
                    Description = book.Description,
                    Price = book.Price,
                    PublishedOn = book.PublishedOn,
                    PublisherId = book.PublisherId
                })
                .ToListAsync();

            return result;
        }
    }
}
