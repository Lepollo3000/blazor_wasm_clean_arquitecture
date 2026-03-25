using BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;
using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Models.StronglyTypedIds;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Models;

public class ApplicationUser : StrongIdentityUser<UserId>
{
    public ApplicationUser() : base(UserId.New()) { }

    public ApplicationUser(string email) : base(UserId.New()) { Email = email; }
}
