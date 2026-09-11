using System.IdentityModel.Tokens.Jwt;
using WebApplication1.DTOs.Auth;
using WebApplication1.model;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services;

public sealed class AuthService : IAuthService
{

    private readonly TokenService _tokenService;
    public AuthService(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public AuthResponse Login(
    LoginRequest request,
    CancellationToken cancellationToken)
    {
        // 1. Find user by email
        var student =
            DataSimulation.clsDataSimulation.Students
                .FirstOrDefault(s => s.Email == request.Email);

        // 2. Check if user exists
        if (student is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // 3. Verify password
        bool passwordValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                student.HashPassword
            );

        if (!passwordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // 4. Generate access token
        string accessToken =
            _tokenService.GenerateAccessToken(student);

        // 5. Generate refresh token
        string refreshToken =
            _tokenService.GenerateRefreshToken();

        // 6. Save refresh token hash
        student.RefreshTokenHash =
            BCrypt.Net.BCrypt.HashPassword(refreshToken);

        student.RefreshTokenExpiresAt =
            DateTime.UtcNow.AddMinutes(15);

        student.RefreshTokenRevokedAt = null;

        // 7. Return tokens
        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }


    public AuthResponse? RefreshToken(
    string refreshToken,
    CancellationToken cancellationToken)
    {
        // 1. Validate refresh token
        if (string.IsNullOrEmpty(refreshToken))
            return null;

        // 2. Find student using refresh token
        var student =
            DataSimulation.clsDataSimulation.Students
                .FirstOrDefault(student =>
                    !string.IsNullOrEmpty(student.RefreshTokenHash) &&
                    BCrypt.Net.BCrypt.Verify(
                        refreshToken,
                        student.RefreshTokenHash
                    )
                );

        // 3. Check if token exists
        if (student is null)
            return null;

        // 4. Check if token is revoked
        if (student.RefreshTokenRevokedAt is not null)
            return null;

        // 5. Check if token is expired
        if (student.RefreshTokenExpiresAt <= DateTime.UtcNow)
            return null;

        // 6. Generate new access token
        string accessToken =
            _tokenService.GenerateAccessToken(student);

        // 7. Rotate refresh token
        string newRefreshToken =
            _tokenService.GenerateRefreshToken();

        // 8. Store hash of new refresh token
        student.RefreshTokenHash =
            BCrypt.Net.BCrypt.HashPassword(newRefreshToken);

        student.RefreshTokenExpiresAt =
            DateTime.UtcNow.AddMinutes(15);

        student.RefreshTokenRevokedAt = null;

        // 9. Return tokens
        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
    }


    public bool Logout(
    string refreshToken,
    CancellationToken cancellationToken)
    {
        // 1. Validate refresh token
        if (string.IsNullOrEmpty(refreshToken))
            return false;

        // 2. Find the student by refresh token
        var student =
            DataSimulation.clsDataSimulation.Students
                .FirstOrDefault(student =>
                    !string.IsNullOrEmpty(student.RefreshTokenHash) &&
                    BCrypt.Net.BCrypt.Verify(
                        refreshToken,
                        student.RefreshTokenHash
                    )
                );

        // 3. Check if token was found
        if (student is null) return false;

        // 4. Revoke refresh token
        student.RefreshTokenRevokedAt = DateTime.UtcNow;

        return true;

    }

}
