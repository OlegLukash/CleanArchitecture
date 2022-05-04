using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Application.App.Publishers.Queries;
using OnlineBookShop.Application.App.Publishers.Responses;

namespace OnlineBookShop.API.Controllers
{
    [Route("api/publishers")]
    public class PublishersController : AppBaseController
    {
        private readonly IMediator _mediator;

        public PublishersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PublisherListDto>> GetAllPublishers()
        {
            var publishers = await _mediator.Send(new GetAllPublishersQuery());
            return publishers;
        }
    }
}
