using Domain.Entities.Auth;

namespace Application.Abstracts.Repositories.Auth;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetByTokenWithUserAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task<bool> DeleteByTokenAsync(string token);
}
