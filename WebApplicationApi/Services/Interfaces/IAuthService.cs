
using WebApplication1.DTOs.Auth;

namespace WebApplication1.Services.Interfaces;

public interface IAuthService
{
    AuthResponse Login(
        LoginRequest request,
        CancellationToken cancellationToken);

    AuthResponse? RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken);

    bool Logout(
        string refreshToken,
        CancellationToken cancellationToken);
}
