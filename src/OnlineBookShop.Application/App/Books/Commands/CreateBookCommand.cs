using AutoMapper;
using MediatR;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Application.Common.Interfaces;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Domain;
using OnlineBookShop.Application.Common.Exceptions;

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
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(IMapper mapper, IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var dbBook = await _bookRepository.FindByAsync<Book>(book => book.Title == request.Title && book.PublisherId == request.PublisherId);
            if (dbBook != null)
            {
                throw new BusinessValidationException("Book for the specified publisher already exists");
            }

            var book = _mapper.Map<Book>(request);
            _bookRepository.Add(book);
            await _unitOfWork.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;
        }
    }
}
