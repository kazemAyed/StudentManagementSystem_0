using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Auth;

public sealed class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
