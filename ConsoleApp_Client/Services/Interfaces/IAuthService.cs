using ConsoleApp_ClintTest_0.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ClintTest_0.Services.Interfaces
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }

        string? AccessToken { get; }

        Task<bool> LoginAsync(string Email, string Password);

        Task<bool> RefreshAsync();

        Task LogoutAsync();
    }
}
