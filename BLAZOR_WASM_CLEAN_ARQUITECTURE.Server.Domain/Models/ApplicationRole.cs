using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Models.StronglyTypedIds;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;

public class ApplicationRole : StrongIdentityRole<UserId>
{
    private ApplicationRole() { }

    public ApplicationRole(string name) : base(UserId.New(), name) { }
}
