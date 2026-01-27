using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers;

public class SeedData
{
    private static readonly IEnumerable<SeedUser> seedUsers =
    [
        new SeedUser()
        {
            Email = "leela@contoso.com", 
            NormalizedEmail = "LEELA@CONTOSO.COM", 
            NormalizedUserName = "LEELA@CONTOSO.COM", 
            RoleList = [ "Administrator"/*, "Manager"*/ ], 
            UserName = "leela@contoso.com"
        },
        new SeedUser()
        {
            Email = "harry@contoso.com",
            NormalizedEmail = "HARRY@CONTOSO.COM",
            NormalizedUserName = "HARRY@CONTOSO.COM",
            RoleList = [],
            UserName = "harry@contoso.com"
        },
    ];

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Users.Any())
        {
            return;
        }

        var userStore = new ApplicationUserStore(context);
        var password = new PasswordHasher<ApplicationUser>();

        using var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        string[] roles = [ "Administrator"/*, "Manager", "User"*/ ];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new(role));
            }
        }

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var seedUser in seedUsers)
        {
            var hashed = password.HashPassword(seedUser, "Passw0rd!");
            seedUser.PasswordHash = hashed;
            await userStore.CreateAsync(seedUser);

            if (seedUser.Email is not null)
            {
                var appUser = await userManager.FindByEmailAsync(seedUser.Email);

                if (appUser is not null && seedUser.RoleList is not null)
                {
                    await userManager.AddToRolesAsync(appUser, seedUser.RoleList);
                }
            }
        }

        await context.SaveChangesAsync();
    }

    private class SeedUser : ApplicationUser
    {
        public string[]? RoleList { get; set; }
    }
}
