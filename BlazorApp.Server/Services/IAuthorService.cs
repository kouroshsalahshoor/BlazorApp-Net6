using BlazorApp.Server.Services.Base;

namespace BlazorApp.Server.Services
{
    public interface IAuthorService
    {
        Task<Response<List<Author>>> Get();
        Task<Response<Author>> GetById(int id);
        Task<Response<Author>> Create(AuthorCreateEditDto model);
        Task<Response<Author>> Edit(int id, AuthorCreateEditDto model);
        Task<Response<Author>> Delete(int id);

    }
}