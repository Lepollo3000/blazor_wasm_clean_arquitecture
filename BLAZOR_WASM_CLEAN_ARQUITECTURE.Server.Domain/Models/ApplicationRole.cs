using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;
using Microsoft.AspNetCore.Identity;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;

public class ApplicationRole : IdentityRole<IdentityKey>
{
    public ApplicationRole() : base() { }

    public ApplicationRole(string roleName)
    {
        Name = roleName;
    }
}
