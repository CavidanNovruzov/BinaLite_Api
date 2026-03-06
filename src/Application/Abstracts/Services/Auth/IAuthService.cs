

using Application.Common.Results;
using Application.Dtos.Auth;
using Domain.Entities.Auth;



namespace Application.Abstracts.Services.Auth
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegistrRequest request, CancellationToken ct = default);

        Task<Result<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);

        Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken);

        Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken ct = default);
    }
}
