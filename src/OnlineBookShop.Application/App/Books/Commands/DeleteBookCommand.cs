using MediatR;
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
        private readonly IRepository _repository;

        public DeleteBookCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete<Book>(request.Id);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
