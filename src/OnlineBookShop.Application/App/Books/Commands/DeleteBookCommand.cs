using MediatR;
using OnlineBookShop.Application.Common.Exceptions;
using OnlineBookShop.Application.Common.Interfaces;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Books.Commands
{
    public class DeleteBookCommand: IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetById<Book>(request.Id);
            if (book == null) throw new EntityNotExistException("Book", request.Id);

            await _bookRepository.Delete<Book>(request.Id);
            await _unitOfWork.SaveChangesAsync();
            

            return Unit.Value;
        }
    }
}
