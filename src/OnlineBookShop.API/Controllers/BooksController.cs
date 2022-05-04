using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Application.App.Books.Commands;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.App.Books.Queries;
using OnlineBookShop.Application.Common.Models;

namespace OnlineBookShop.API.Controllers
{
    [Route("api/books")]
    public class BooksController : AppBaseController
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("paginated-search")]
        public async Task<PaginatedResult<BookListDto>> GetPagedBooks(PagedRequest pagedRequest)
        {
            var response = await _mediator.Send(new GetBooksPagedQuery() { PagedRequest = pagedRequest });
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BookDto> GetBook(int id)
        {
            var bookDto = await _mediator.Send(new GetBookByIdQuery() { BookId = id });
            return bookDto;
        }

        [HttpPost]
        public async Task<BookDto> CreateBook(CreateBookCommand bookForUpdateDto)
        {
            var bookDto = await _mediator.Send(bookForUpdateDto);
            return bookDto;
        }

        [HttpPut]
        public async Task UpdateBook(UpdateBookCommand updateBookCommand)
        {
            await _mediator.Send(updateBookCommand);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBook(int id)
        {
            await _mediator.Send(new DeleteBookCommand() { Id = id });
        }
    }
}
