using Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Options;

public class ConfigureJwtBearerOptions : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwt;

    public ConfigureJwtBearerOptions(IOptions<JwtOptions> jwtOptions)
    {
        _jwt = jwtOptions.Value;
    }
    public void Configure(string? name, JwtBearerOptions options)
    {
        // Default scheme üçün də işləsin
        Configure(options);
    }
    public void Configure(JwtBearerOptions options)
    {
        Console.WriteLine("VALIDATION SECRET: " + _jwt.Secret);

        if (string.IsNullOrWhiteSpace(_jwt.Secret))
            throw new InvalidOperationException("JwtOptions.Secret is missing");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwt.Issuer,
            ValidAudience = _jwt.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret)),

            ClockSkew = TimeSpan.Zero
        };
    }
}
