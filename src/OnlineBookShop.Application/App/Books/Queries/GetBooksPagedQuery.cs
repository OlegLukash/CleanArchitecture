using AutoMapper;
using MediatR;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Application.Common.Models;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Books.Queries
{
    public class GetBooksPagedQuery: IRequest<PaginatedResult<BookListDto>>
    {
        public PagedRequest PagedRequest { get; set; }
    }

    public class GetBooksPagedQueryHandler : IRequestHandler<GetBooksPagedQuery, PaginatedResult<BookListDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksPagedQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<PaginatedResult<BookListDto>> Handle(GetBooksPagedQuery request, CancellationToken cancellationToken)
        {
            var pagedBooksDto = await _bookRepository.GetPagedData<Book, BookListDto>(request.PagedRequest);
            return pagedBooksDto;
        }
    }
}
