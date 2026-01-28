using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, IdentityKey>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseIdentityKeyAsInt();

        modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<ApplicationRole>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.UseStronglyTypedIds();
    }
}
