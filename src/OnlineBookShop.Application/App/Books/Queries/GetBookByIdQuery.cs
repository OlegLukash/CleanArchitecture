using AutoMapper;
using MediatR;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Books.Queries
{
    public class GetBookByIdQuery: IRequest<BookDto>
    {
        public int BookId { get; set; }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdWithInclude<Book>(request.BookId, book => book.Publisher);
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }
    }
}
