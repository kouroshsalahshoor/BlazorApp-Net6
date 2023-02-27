using AutoMapper;
using BlazorApp.API.Data;

namespace BlazorApp.API.Infrastructure
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateEditDto, Author>().ReverseMap();

            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
        }
    }
}
