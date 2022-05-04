using AutoMapper;
using MediatR;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Repositories;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Books.Commands
{
    public class CreateBookCommand: IRequest<BookDto>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public int PublisherId { get; set; }

        public decimal Price { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public CreateBookCommandHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            _repository.Add(book);
            await _repository.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;
        }
    }
}
