
using Application.Abstracts.Repositories.Auth;
using Application.Abstracts.Services.Auth;
using Application.Options;
using Domain.Entities.Auth;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Persistance.Services.Auth;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtOptions _jwtOptions;
    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IOptions<JwtOptions> jwtOptions)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtOptions = jwtOptions.Value;
    }
    public async Task<string> CreateRefreshTokenAsync(User user)
    {
        // 32 byte secure random
        var rawToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        var hashedToken = Convert.ToHexString(
        SHA256.HashData(Encoding.UTF8.GetBytes(rawToken))
    );

        var refreshToken = new RefreshToken
        {
            Token = hashedToken,
            UserId = user.Id,
            User = user,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshExpirationMinutes)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        return rawToken;
    }
    public async Task<User?> ValidateAndConsumeAsync(string token)
    {
        var hashedToken = Convert.ToHexString(
        SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        var refreshToken = await _refreshTokenRepository.GetByTokenWithUserAsync(hashedToken);

        if (refreshToken == null)
            return null;

        if (refreshToken.ExpiresAtUtc < DateTime.UtcNow)
        {
            await _refreshTokenRepository.DeleteByTokenAsync(hashedToken);
            return null;
        }

        var deleted=await _refreshTokenRepository.DeleteByTokenAsync(hashedToken);
        if (!deleted)
            return null;

        return refreshToken.User;
    }

}
