using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;

public class ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
: UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, StrongId<ApplicationUser>>(context, describer)
{
}
