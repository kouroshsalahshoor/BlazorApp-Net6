using BlazorApp.Server.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorApp.Server.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;

        public AuthorService(IClient client, ProtectedLocalStorage protectedLocalStorage) : base(client, protectedLocalStorage)
        {
            _client = client;
        }

        public async Task<Response<AuthorDto>> Create(AuthorCreateEditDto createDto)
        {
            Response<AuthorDto> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorPOSTAsync(createDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorDto>(ex);
            }

            return response;
        }

        public async Task<Response<AuthorDto>> Delete(int id)
        {
            Response<AuthorDto> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorDELETEAsync(id);
                response.Success= true;
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorDto>(ex);
            }

            return response;
        }

        public async Task<Response<AuthorDto>> Edit(int id, AuthorCreateEditDto editDto)
        {
            Response<AuthorDto> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorPUTAsync(id, editDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorDto>(ex);
            }

            return response;
        }

        public async Task<Response<List<AuthorDto>>> Get()
        {
            Response<List<AuthorDto>> response = new Response<List<AuthorDto>> { Success = true };

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsAsync();
                response.Data = data.ToList();
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<List<AuthorDto>>(ex);
            }

            return response;
        }

        public async Task<Response<AuthorDto>> Get(int id)
        {
            Response<AuthorDto> response;

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorGETAsync(id);
                response = new Response<AuthorDto>
                {
                    Success = true,
                    Data = data
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorDto>(ex);
            }

            return response;
        }

    }
}
