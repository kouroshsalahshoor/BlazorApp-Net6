using AutoMapper;
using BlazorApp.Server.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorApp.Server.Services
{
    public class BookService : BaseHttpService, IBookService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;

        public BookService(IClient client, ProtectedLocalStorage protectedLocalStorage, IMapper mapper) : base(client, protectedLocalStorage)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Response<BookDto>> Create(BookCreateEditDto createDto)
        {
            Response<BookDto> response = new();

            try
            {
                await GetBearerToken();
                var dto = _mapper.Map<BookCreateEditDto>(createDto);
                await _client.BookPOSTAsync(dto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<BookDto>(ex);
            }

            return response;
        }

        public async Task<Response<BookDto>> Delete(int id)
        {
            Response<BookDto> response = new();

            try
            {
                await GetBearerToken();
                await _client.BookDELETEAsync(id);
                response.Success= true;
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<BookDto>(ex);
            }

            return response;
        }

        public async Task<Response<BookDto>> Edit(int id, BookCreateEditDto editDto)
        {
            Response<BookDto> response = new();

            try
            {
                await GetBearerToken();

                var dto = _mapper.Map<BookCreateEditDto>(editDto);
                await _client.BookPUTAsync(id, dto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<BookDto>(ex);
            }

            return response;
        }

        public async Task<Response<List<BookDto>>> Get()
        {
            Response<List<BookDto>> response = new Response<List<BookDto>> { Success = true };

            try
            {
                await GetBearerToken();
                var data = await _client.BooksAsync();
                response.Data = data.ToList();
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<List<BookDto>>(ex);
            }

            return response;
        }

        public async Task<Response<BookDto>> Get(int id)
        {
            Response<BookDto> response;

            try
            {
                await GetBearerToken();
                var data = await _client.BookGETAsync(id);
                response = new Response<BookDto>
                {
                    Success = true,
                    Data = data
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<BookDto>(ex);
            }

            return response;
        }

    }
}
