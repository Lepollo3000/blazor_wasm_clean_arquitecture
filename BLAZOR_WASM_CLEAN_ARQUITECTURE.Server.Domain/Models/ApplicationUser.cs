using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.AspNetCore.Identity;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;

public class ApplicationUser : IdentityUser<StrongId<ApplicationUser>>
{
}
