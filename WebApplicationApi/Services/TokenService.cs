using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.model;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services;

public sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Generates a JWT access token for the specified student.
    /// </summary>
    public string GenerateAccessToken(Students student)
    {
        
        var jwtKey = 
            _configuration["Jwt:Key"] ?? 
            throw new InvalidOperationException("JWT key is not configured.");

        var issuer = 
            _configuration["Jwt:Issuer"] ?? 
            throw new InvalidOperationException("JWT issuer is not configured.");

        var audience = 
            _configuration["Jwt:Audience"] ?? 
            throw new InvalidOperationException("JWT audience is not configured.");

        var expirationMinutes = GetAccessTokenExpirationMinutes();

        var claims = 
            new List<Claim> 
            {
                new Claim
                (
                    "sub",
                    student.Id.ToString(),
                    ClaimValueTypes.Integer
                ),
                new Claim
                (
                    "email",
                    student.Email!.ToString(),
                    ClaimValueTypes.Email
                ),
                new Claim
                (
                    "jti",
                    Guid.NewGuid().ToString(),
                    ClaimValueTypes.String
                ),
               new Claim
               (
                   ClaimTypes.Role,
                   student.Role.ToString(),
                   ClaimValueTypes.String
               )
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// </summary>
    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    /// <summary>
    /// Gets the claims principal from an expired JWT access token.
    /// </summary>
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,

            ValidIssuer = _configuration["Jwt:Issuer"],

            ValidateAudience = true,

            ValidAudience = _configuration["Jwt:Audience"],

            ValidateLifetime = false,

            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = 
                tokenHandler.ValidateToken
                (
                    token,
                    tokenValidationParameters,
                    out var securityToken
                );

            if (securityToken is not JwtSecurityToken jwtSecurityToken)
            {
                return null;
            }

            if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase)) 
            {
                return null;
            }

            return principal;
        }
        catch (SecurityTokenException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    private int GetAccessTokenExpirationMinutes()
    {
        return int.TryParse(_configuration["Jwt:AccessTokenExpirationMinutes"], out var minutes) ? minutes : 15;
    }

}
