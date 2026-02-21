using Application.Abstracts.Repositories.Auth;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories.Auth
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly BinaLiteDbContext _context;
        public RefreshTokenRepository(BinaLiteDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> GetByTokenWithUserAsync(string token)
        {
            return await _context.RefreshTokens
       .AsNoTracking()
       .Include(rt => rt.User)
       .FirstOrDefaultAsync(rt => rt.Token == token);
        }
        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByTokenAsync(string token)
        {
            var affectedRows = await _context.RefreshTokens
                .Where(rt => rt.Token == token)
                .ExecuteDeleteAsync();

            return affectedRows > 0;
        }

    }
}
