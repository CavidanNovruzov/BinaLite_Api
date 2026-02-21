using Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Persistance.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in new[] { RoleNames.Admin, RoleNames.User })
            {
                if (await roleManager.RoleExistsAsync(roleName))
                    continue;

                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

        }

    }
}
