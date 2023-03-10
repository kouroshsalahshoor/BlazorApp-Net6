using AutoMapper;
using BlazorApp.API.Data;

namespace BlazorApp.API.Infrastructure
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<AuthorCreateEditDto, Author>().ReverseMap();


            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<BookCreateEditDto, Book>().ReverseMap();

            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
        }
    }
}
