using AutoMapper;
using OnlineBookShop.Application.App.Publishers.Responses;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherListDto>();
        }
    }
}
