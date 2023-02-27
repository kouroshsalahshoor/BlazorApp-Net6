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

        public async Task<Response<Author>> Create(AuthorCreateEditDto model)
        {
            Response<Author> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorPOSTAsync(model);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<Author>(ex);
            }

            return response;
        }

        public async Task<Response<Author>> Delete(int id)
        {
            Response<Author> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorDELETEAsync(id);
                response.Success= true;
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<Author>(ex);
            }

            return response;
        }

        public async Task<Response<Author>> Edit(int id, AuthorCreateEditDto model)
        {
            Response<Author> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorPUTAsync(id, model);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<Author>(ex);
            }

            return response;
        }

        public async Task<Response<List<Author>>> Get()
        {
            Response<List<Author>> response = new Response<List<Author>> { Success = true };

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsAsync();
                response.Data = data.ToList();
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<List<Author>>(ex);
            }

            return response;
        }

        public async Task<Response<Author>> GetById(int id)
        {
            Response<Author> response;

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorGETAsync(id);
                response = new Response<Author>
                {
                    Success = true,
                    Data = data
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<Author>(ex);
            }

            return response;
        }

    }
}
