using API.Middleware;
using Application.Options;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistance.Data;

namespace API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> ConfigurePipeLine(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();   
        await RoleSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());  

        if (app.Environment.IsDevelopment())
        {
            await AdminSeeder.SeedAsync(
                scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
              scope.ServiceProvider.GetRequiredService<IOptions<SeedOptions>>()
                );
        }


        app.UseExceptionHandling();
        app.UseStaticFiles();
        // Pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BinaLite API v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        return app; 
    }
}
