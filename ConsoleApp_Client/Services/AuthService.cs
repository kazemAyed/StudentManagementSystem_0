using ConsoleApp_ClintTest_0.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_ClintTest_0.Api;
using ConsoleApp_ClintTest_0.Services.Interfaces;
using System.Net.Http.Json;

namespace ConsoleApp_ClintTest_0.Services
{

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        private string? _refreshToken;

        public string? AccessToken { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken);

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(
            string Email,
            string Password)
        {

            LoginRequest request = new LoginRequest()
            {
                Email = Email,
                Password = Password
            };

            var response = await _httpClient.PostAsJsonAsync(
                $"https://localhost:7079/api/auth/login",
                request);

            if (!response.IsSuccessStatusCode)
                return false;

            var result =
                await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null)
                return false;

            AccessToken = result.AccessToken;
            _refreshToken = result.RefreshToken;

            return true;
        }

        public async Task<bool> RefreshAsync()
        {
            if (string.IsNullOrWhiteSpace(_refreshToken))
                return false;

            var request = new RefreshTokenRequest
            {
                RefreshToken = _refreshToken
            };

            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/refresh",
                request);

            if (!response.IsSuccessStatusCode)
            {
                await LogoutAsync();
                return false;
            }

            var result =
                await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null)
            {
                await LogoutAsync();
                return false;
            }

            AccessToken = result.AccessToken;
            _refreshToken = result.RefreshToken;

            return true;
        }

        public async Task LogoutAsync()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_refreshToken))
                {
                    var request = new RefreshTokenRequest
                    {
                        RefreshToken = _refreshToken
                    };

                    await _httpClient.PostAsJsonAsync(
                        "https://localhost:7079/api/Auth/logout",
                        request);
                }
            }
            finally
            {
                AccessToken = null;
                _refreshToken = null;
            }
        }
    }


}
