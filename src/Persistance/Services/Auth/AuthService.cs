using Application.Abstracts.Services.Auth;
using Application.Common.Results;
using Application.Dtos.Auth;
using Application.Options;
using Domain.Constants;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace Persistance.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IEmailSender _emailSender;
    private readonly SmtpOptions _emailOptions;
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
        IOptions<JwtOptions> jwtOptions,
        IEmailSender emailSender,          
        IOptions<SmtpOptions> emailOptions 
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _refreshTokenService = refreshTokenService;
        _jwtOptions = jwtOptions.Value;

        _emailSender = emailSender;
        _emailOptions = emailOptions.Value;
    }
    public async Task<Result> RegisterAsync(RegistrRequest request, CancellationToken ct = default)
    {
        var user = new User
        {
            FullName = request.FullName,
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result.Failure(string.Join(" ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, RoleNames.User);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var link =
            $"{_emailOptions.ConfirmEmailBaseUrl.TrimEnd('/')}" +
            $"?userId={Uri.EscapeDataString(user.Id)}" +
            $"&token={Uri.EscapeDataString(token)}";

        var html = $"<p>Hesabınızı təsdiqləmək üçün <a href=\"{link}\">bu linkə</a> keçid edin.</p>";

        await _emailSender.SendEmailAsync(
            to: user.Email!,
            subject: "Email təsdiqi - BinaLite",
            html,
            plainBody: link,
            ct
        );

        return Result.Success();
    }
    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Login)
            ?? await _userManager.FindByNameAsync(request.Login);

        if (user == null)
            return Result<TokenResponse>.Failure("İstifadəçi tapılmadı.");

        if (!user.EmailConfirmed)
            return Result<TokenResponse>.Failure("Email təsdiqlənməyib.");

        var check = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (!check.Succeeded)
            return Result<TokenResponse>.Failure("Şifrə yanlışdır.");

        var token = await BuildTokenResponseAsync(user);

        return Result<TokenResponse>.Success(token);
    }

    public async Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken)
    {
        var user = await _refreshTokenService.ValidateAndConsumeAsync(refreshToken);

        if (user == null)
            return Result<TokenResponse>.Failure("Refresh token etibarsız və ya vaxtı keçib.");

        var token = await BuildTokenResponseAsync(user);

        return Result<TokenResponse>.Success(token);
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

    public async Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Result.Failure("İstifadəçi tapılmadı.");

        if (user.EmailConfirmed)
            return Result.Failure("Email artıq təsdiqlənib.");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            return Result.Failure("Token etibarsız və ya vaxtı keçib.");

        return Result.Success();
    }
}
