using Application.Abstracts.Services.Auth;
using Application.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Entities.Auth;

namespace Infrastructure.Services;

public class JwtTokenGenerator: IJwtTokenGenerator
{
    private readonly JwtOptions _options;
    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateAccessToken(User user,IEnumerable<string> roles)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
        new Claim(JwtRegisteredClaimNames.Sub,user.Id),
        new Claim(JwtRegisteredClaimNames.Email,user.Email ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        new Claim("fullName",user.FullName ?? string.Empty)
        };
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
            notBefore: DateTime.UtcNow,
            signingCredentials: credentials
        );
     
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
