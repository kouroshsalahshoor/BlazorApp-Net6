using BlazorApp.Server.Services.Base;

namespace BlazorApp.Server.Services
{
    public interface IBookService
    {
        Task<Response<List<BookDto>>> Get();
        Task<Response<BookDto>> Get(int id);
        Task<Response<BookDto>> Create(BookCreateEditDto createDto);
        Task<Response<BookDto>> Edit(int id, BookCreateEditDto editDto);
        Task<Response<BookDto>> Delete(int id);

    }
}