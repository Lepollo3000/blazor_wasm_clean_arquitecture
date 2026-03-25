using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers;

public class SeedData
{
    private static readonly IEnumerable<SeedUser> seedUsers =
    [
        new SeedUser("leela@contoso.com")
        {
            NormalizedEmail = "LEELA@CONTOSO.COM",
            NormalizedUserName = "LEELA@CONTOSO.COM",
            RoleList = ["Administrator", "Manager"],
            UserName = "leela@contoso.com"
        },
        new SeedUser("harry@contoso.com")
        {
            NormalizedEmail = "HARRY@CONTOSO.COM",
            NormalizedUserName = "HARRY@CONTOSO.COM",
            RoleList = ["User"],
            UserName = "harry@contoso.com"
        },
    ];

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        await context.Database.MigrateAsync();

        var password = new PasswordHasher<ApplicationUser>();

        using var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        string[] roles = ["Administrator", "Manager", "User"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new(role));
            }
        }

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (SeedUser seedUser in seedUsers)
        {
            if (seedUser.Email is not null)
            {
                seedUser.SecurityStamp = Guid.NewGuid().ToString("N");

                var user = await userManager.FindByEmailAsync(seedUser.Email);

                if (user is null)
                {
                    await userManager.CreateAsync(user: seedUser, password: "Pa55w.rd");

                    user = await userManager.FindByEmailAsync(seedUser.Email);
                }

                if (user is not null && seedUser.RoleList is not null)
                {
                    await userManager.UpdateSecurityStampAsync(user);

                    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    await userManager.ConfirmEmailAsync(user: user, token: token);

                    await userManager.AddToRolesAsync(user, seedUser.RoleList);
                }
            }
        }

        await context.SaveChangesAsync();
    }

    private class SeedUser(string email) : ApplicationUser(email)
    {
        public string[]? RoleList { get; set; }
    }
}
