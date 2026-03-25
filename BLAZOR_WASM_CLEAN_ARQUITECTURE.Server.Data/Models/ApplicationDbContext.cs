using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers.StronglyTypedIds;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Models.StronglyTypedIds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.IgnoreStrongIds(
            typeof(ApplicationDbContext).Assembly,
            typeof(UserId).Assembly);

        modelBuilder.ApplyStrongIdConversions();
    }
}
