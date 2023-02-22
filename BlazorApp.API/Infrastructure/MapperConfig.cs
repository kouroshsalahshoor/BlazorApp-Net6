using AutoMapper;
using BlazorApp.API.Data;

namespace BlazorApp.API.Infrastructure
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>();

            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
        }
    }
}
