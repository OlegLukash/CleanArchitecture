using AutoMapper;
using MediatR;
using OnlineBookShop.Application.App.Publishers.Responses;
using OnlineBookShop.Application.Common.Interfaces.Repositories;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.App.Publishers.Queries
{
    public class GetAllPublishersQuery: IRequest<IEnumerable<PublisherListDto>>
    {

    }

    public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherListDto>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPublishersQueryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublisherListDto>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
        {
            var publisherList = await _repository.GetAll<Publisher>();
            var publisherDtoList = _mapper.Map<List<PublisherListDto>>(publisherList);
            return publisherDtoList;
        }
    }
}
