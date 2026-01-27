using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, StrongId<ApplicationUser>>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseStronglyTypedIds();

        base.OnModelCreating(modelBuilder);
    }

}
