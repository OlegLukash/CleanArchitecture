using MediatR;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Common.Interfaces.Repositories;

namespace OnlineBookShop.Application.App.Books.Queries
{
    public class GetTopRatedBooksQuery: IRequest<IEnumerable<BookDto>>
    {

    }

    public class GetTopRatedBooksQueryHandler : IRequestHandler<GetTopRatedBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetTopRatedBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetTopRatedBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetTopRatedBooks();
        }
    }
}
