using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.DbContext;

public class ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
: UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, IdentityKey>(context, describer)
{
}
