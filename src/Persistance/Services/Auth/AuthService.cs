using Application.Abstracts.Services.Auth;
using Application.Dtos.Auth;
using Application.Options;
using Domain.Constants;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace Persistance.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly JwtOptions _jwtOptions;

    public AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IJwtTokenGenerator jwtGenerator,
    IRefreshTokenService refreshTokenService,
    IOptions<JwtOptions> jwtOptions
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _refreshTokenService = refreshTokenService;
        _jwtOptions = jwtOptions.Value;
    }
    public async Task<(bool Success, string? Error)> RegisterAsync(RegistrRequest request, CancellationToken ct = default)
    {
        var user = new User
        {
            FullName = request.FullName,
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return (false, string.Join(" ", result.Errors.Select(e => e.Description)));
        await _userManager.AddToRoleAsync(user, RoleNames.User);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var link =
            $"{_emailOptions.ConfirmEmailBaseUrl.TrimEnd('/')}?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token)}";

        var html = $"<p>Hesabınızı təsdiqləmək üçün <a href=\"{link}\">bu linkə</a> keçid edin.</p>";

        await _emailSender.SendEmailAsync(
            to: user.Email!,
            subject: "Email təsdiqi - BinaLite",
            html,
            plainBody: link,
            ct
        );

        return (true, null);
    }
    public async Task<TokenResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Login)
            ?? await _userManager.FindByNameAsync(request.Login);
        if (user == null)
            return null;
        var ok = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
        if (!ok.Succeeded)
            return null;
        return await BuildTokenResponseAsync(user);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var user = await _refreshTokenService.ValidateAndConsumeAsync(refreshToken);
        if (user == null)
            return null;

        return await BuildTokenResponseAsync(user);
    }

    public async Task<TokenResponse?> BuildTokenResponseAsync(User user, CancellationToken ct = default)
    {
        var roles =await _userManager.GetRolesAsync(user);
        var accessToken = _jwtGenerator.GenerateAccessToken(user, roles);
        var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAtUtc = expiresAt
        };
    }
}
