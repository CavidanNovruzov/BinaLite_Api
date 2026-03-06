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

    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegistrRequest request, CancellationToken ct)
    {
        var result = await _authService.RegisterAsync(request, ct);

        if (!result.IsSuccess)
            return BadRequest(BaseResponse.Fail(result.Error!));

        return Ok(BaseResponse.Ok("User registered successfully. Email təsdiq linki göndərildi."));
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(request, ct);

        if (!result.IsSuccess)
            return BadRequest(BaseResponse.Fail(result.Error!));

        return Ok(BaseResponse<TokenResponse>.Ok(result.Value));
    }


    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return BadRequest(BaseResponse.Fail("Refresh token is required"));

        var result = await _authService.RefreshTokenAsync(request.RefreshToken);

        if (!result.IsSuccess)
            return Unauthorized(BaseResponse.Fail(result.Error!));

        return Ok(BaseResponse<TokenResponse>.Ok(result.Value));
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

        var result = await _authService.ConfirmEmailAsync(userId, token, ct);

        if (!result.IsSuccess)
            return BadRequest(BaseResponse.Fail(result.Error!));

        return Ok(BaseResponse.Ok("Email təsdiqləndi."));
    }
}