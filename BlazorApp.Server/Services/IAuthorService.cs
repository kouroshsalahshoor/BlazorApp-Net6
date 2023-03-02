using BlazorApp.Server.Services.Base;

namespace BlazorApp.Server.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorDto>>> Get();
        Task<Response<AuthorDto>> Get(int id);
        Task<Response<AuthorDto>> Create(AuthorCreateEditDto createDto);
        Task<Response<AuthorDto>> Edit(int id, AuthorCreateEditDto editDto);
        Task<Response<AuthorDto>> Delete(int id);

    }
}