using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.AspNetCore.Identity;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;

public class ApplicationRole : IdentityRole<StrongId<ApplicationUser>>
{
    public ApplicationRole() : base() { }

    public ApplicationRole(string roleName)
    {
        Name = roleName;
    }
}
