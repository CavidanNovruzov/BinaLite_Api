using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Auth;

public class User:IdentityUser
{
    public string FullName { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}
