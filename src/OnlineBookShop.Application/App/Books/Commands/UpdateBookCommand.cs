using AutoMapper;
using MediatR;
using OnlineBookShop.Application.Repositories;
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
        private readonly IRepository _repository;

        public UpdateBookCommandHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById<Book>(request.Id);
            _mapper.Map(request, book);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
