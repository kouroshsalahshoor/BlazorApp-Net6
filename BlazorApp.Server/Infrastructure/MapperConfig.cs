using AutoMapper;
using BlazorApp.Server.Services.Base;

namespace BlazorApp.Server.Infrastructure
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateEditDto, Author>().ReverseMap();
            CreateMap<AuthorCreateEditDto, AuthorDto>().ReverseMap();

            CreateMap<BookCreateEditDto, Book>().ReverseMap();
            CreateMap<BookCreateEditDto, BookDto>().ReverseMap();

        }
    }
}
