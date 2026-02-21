using Application.Options;
using Domain.Constants;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Persistance.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(UserManager<User> userManager,IOptions<SeedOptions> options)
    {
        var seed=options.Value.Admin;
        if (string.IsNullOrWhiteSpace(seed.AdminEmail) || string.IsNullOrWhiteSpace(seed.AdminPassword))
            return;

        var existing=await userManager.FindByEmailAsync(seed.AdminEmail);
        if (existing != null) 
            return;

        var admin = new User
        {
            UserName = seed.AdminEmail,
            Email = seed.AdminEmail,
            FullName = "Admin",
            EmailConfirmed =true
        };

        var result = await userManager.CreateAsync(admin, seed.AdminPassword);  
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, RoleNames.Admin);
        }

    }
}
