using System.Security.Claims;
using WebApplication1.model;

namespace WebApplication1.Services.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// Generates a JWT access token for the specified student.
    /// </summary>
    /// <param name="student">The authenticated student.</param>
    /// <returns>A signed JWT access token.</returns>
    string GenerateAccessToken(Students student);

    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// </summary>
    /// <returns>A secure refresh token.</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Gets the claims principal from an expired access token.
    /// </summary>
    /// <param name="token">The expired JWT access token.</param>
    /// <returns>The claims principal if the token is valid; otherwise null.</returns>
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
