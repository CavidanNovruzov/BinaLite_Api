
using API.Options;
using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.Auth;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Auth;
using Application.Options;
using Application.Validations.PropertyAd;
using Domain.Constants;
using Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.Repositories.Auth;
using Persistance.Services;
using Persistance.Services.Auth;
using System.Text;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Controllers
        services.AddControllers();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreatePropertyAdRequestValidator>();

        // DbContext
        services.AddDbContext<BinaLiteDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<BinaLiteDbContext>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.Configure<SeedOptions>(configuration.GetSection(SeedOptions.SectionName));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        // AutoMapper 
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Repositories
        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IPropertyAdRepository, PropertyAdRepository>();
        services.AddScoped<IPropertyMediaRepository, PropertyMediaRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<ICityRepository, CityRepository>();

        // Services
        services.AddScoped<IPropertyAdService, PropertyAdService>();
        services.AddScoped<IPropertyMediaService, PropertyMediaService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
     

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Token daxil edin"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                   Array.Empty<string>()
                }
            });

        });

        // 3) API-lar üçün cookie redirect-ləri söndür (Identity var deyə)
        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = ctx
                =>
            { ctx.Response.StatusCode = StatusCodes.Status401Unauthorized; return Task.CompletedTask; };

            options.Events.OnRedirectToAccessDenied = ctx
                =>
            { ctx.Response.StatusCode = StatusCodes.Status403Forbidden; return Task.CompletedTask; };
        });

        services.AddAuthorization(options =>
        {
            // Şəhərləri idarə etmək üçün policy – yalnız Admin roluna sahib istifadəçilər
            options.AddPolicy(Policies.ManageCities, p =>
             p.RequireRole(RoleNames.Admin));
            // Əmlakları idarə etmək üçün policy – yalnız login olmuş istifadəçilər
            options.AddPolicy(Policies.ManageProperties, p =>
             p.RequireAuthenticatedUser());
        });

        return services;
    }

}
