using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlazorApp.Server.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public BaseHttpService(IClient client, ProtectedLocalStorage protectedLocalStorage)
        {
            _client = client;
            _protectedLocalStorage = protectedLocalStorage;
        }

        protected Response<T> ConvertApiException<T>(ApiException apiException)
        {
            if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var data = JsonSerializer.Deserialize<T>(apiException.Response, options);
                return new Response<T>() { Message = "Success.", Data = data!, Success = true };
            }
            else if (apiException.StatusCode == 400)
            {
                return new Response<T>() { Message = "Validation error.", ValidationErrors = apiException.Response, Success = false };
            }
            else if (apiException.StatusCode == 401)
            {
                return new Response<T>() { Message = "Not Authorized. You are not logged in.", Success = false };
            }
            else if (apiException.StatusCode == 403)
            {
                return new Response<T>() { Message = "Forbidden. Please login with another credentials.", Success = false };
            }
            else if (apiException.StatusCode == 404)
            {
                return new Response<T>() { Message = "Item not found.", Success = false };
            }

            return new Response<T>() { Message = "Something went wrong. Please try agian", Success = false };

        }

        protected async Task GetBearerToken()
        {
            var token = (await _protectedLocalStorage.GetAsync<string>("token")).Value;
            if (token != null)
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
