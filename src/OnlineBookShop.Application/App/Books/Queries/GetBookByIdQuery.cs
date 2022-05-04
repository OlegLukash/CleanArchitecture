using AutoMapper;
using MediatR;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Repositories;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Books.Queries
{
    public class GetBookByIdQuery: IRequest<BookDto>
    {
        public int BookId { get; set; }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdWithInclude<Book>(request.BookId, book => book.Publisher);
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }
    }
}
