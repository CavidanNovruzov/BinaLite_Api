using Application.Abstracts.Services.Auth;
using Application.Dtos.Auth;
using Application.Shared.Helpers.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;


    [HttpPost("registr")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegistrRequest request, CancellationToken ct)
    {
        var (success, error) = await _authService.RegisterAsync(request, ct);
        if (!success)
            return BadRequest(BaseResponse.Fail(error ?? "Registration failed"));
        return Ok(BaseResponse.Ok("User registered successfully"));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var tokenResponse = await _authService.LoginAsync(request, ct);
        if (tokenResponse == null)
            return Unauthorized(BaseResponse.Fail("Invalid login or password"));

        return Ok(BaseResponse<TokenResponse>.Ok(tokenResponse));
    }
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return BadRequest(BaseResponse.Fail("Refresh token is required"));

        var tokenResponse = await _authService.RefreshTokenAsync(request.RefreshToken);
        if (tokenResponse == null)
            return Unauthorized(BaseResponse.Fail("Invalid or expired refresh token"));

        return Ok(BaseResponse<TokenResponse>.Ok(tokenResponse));
    }

    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public async Task<ActionResult<BaseResponse>> ConfirmEmail(
    [FromQuery] string? userId,
    [FromQuery] string? token,
    CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return BadRequest(BaseResponse.Fail("userId və token tələb olunur."));

        var ok = await _authService.ConfirmEmailAsync(userId, token, ct);

        if (!ok)
            return BadRequest(BaseResponse.Fail("Token etibarsız və ya vaxtı keçib."));

        return Ok(BaseResponse.Ok("Email təsdiqləndi."));
    }
}
