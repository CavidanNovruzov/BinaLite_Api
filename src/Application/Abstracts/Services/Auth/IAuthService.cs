

using Application.Dtos.Auth;
using Domain.Entities.Auth;



namespace Application.Abstracts.Services.Auth
{
    public interface IAuthService
    {
        Task<(bool Success, string? Error)> RegisterAsync(RegistrRequest request, CancellationToken ct = default);
        Task<TokenResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default);
        Task<TokenResponse?> RefreshTokenAsync(string refreshToken);

        Task<TokenResponse?> BuildTokenResponseAsync(User user,CancellationToken ct = default);
    }
}
