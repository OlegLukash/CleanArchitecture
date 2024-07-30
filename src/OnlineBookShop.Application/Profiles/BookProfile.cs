using AutoMapper;
using OnlineBookShop.Application.App.Books.Commands;
using OnlineBookShop.Application.App.Books.Dtos;
using OnlineBookShop.Domain;

namespace OnlineBookShop.Application.Profiles
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookListDto>()
                .ForMember(x => x.Publisher, y => y.MapFrom(z => z.Publisher.Name));
            CreateMap<Book, BookDto>();

            CreateMap<CreateBookCommand, Book>();

            CreateMap<UpdateBookCommand, Book>();
        }
    }
}
