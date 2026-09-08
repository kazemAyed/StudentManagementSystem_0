
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebApplication1.DTOs.Auth;
using WebApplication1.model;
using WebApplication1.Services;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates a user and returns access and refresh tokens.
    /// </summary>
    [HttpPost("login")]
    [EnableRateLimiting("api")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<AuthResponse> Login(
    [FromBody] LoginRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            var result = _authService.Login(
                request,
                cancellationToken);

            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }


    /// <summary>
    /// Generates a new access token using a valid refresh token.
    /// </summary>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<AuthResponse> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = _authService.RefreshToken(
            request.RefreshToken,
            cancellationToken);

        if (result is null)
            return Unauthorized();

        return Ok(result);
    }


    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Logout(
      [FromBody] LogoutRequest request,
      CancellationToken cancellationToken)
    {

        // check the refresh token 
        if (string.IsNullOrEmpty(request.RefreshToken)) return Unauthorized();

        // Get the authenticated user's ID from the JWT
        var studentId = User.FindFirst("sub")?.Value;

        // Get the unique JWT ID
        var jti = User.FindFirst("jti")?.Value;

        if (studentId is null || jti is null)
            return Unauthorized();

        // Find the student
        var student = DataSimulation.clsDataSimulation.Students
            .FirstOrDefault(s => s.Id.ToString() == studentId);

        if (student is null)
            return Unauthorized();

        bool vaildRefreshToken = BCrypt.Net.BCrypt.Verify(
            request.RefreshToken,
            student.RefreshTokenHash);

        if (!vaildRefreshToken) return Unauthorized();

        student.RefreshTokenRevokedAt = DateTime.UtcNow;

        // TODO:
        // Add the jti to your revoked-token store.
        // Example:
        // DataSimulation.clsDataSimulation.RevokedTokens.Add(jti);

        return NoContent();

    }



}
