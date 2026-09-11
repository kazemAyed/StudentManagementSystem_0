using ConsoleApp_ClintTest_0.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ClintTest_0.Api
{

    public class WebAppApiClient
    {
        private readonly HttpClient _httpClient;

        public WebAppApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/login",
                request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            //result.

            //return result ?? throw new InvalidOperationException("Invalid login response.");
            return result ?? null;
        }
    }

}
