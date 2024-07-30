using AutoMapper;
using MediatR;
using OnlineBookShop.Application.Common.Exceptions;
using OnlineBookShop.Application.Common.Interfaces;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Books.Commands
{
    public class UpdateBookCommand: IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public int PublisherId { get; set; }

        public decimal Price { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IMapper mapper, IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetById<Book>(request.Id);
            if (book == null) throw new EntityNotExistException("Book", request.Id);

            _mapper.Map(request, book);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
