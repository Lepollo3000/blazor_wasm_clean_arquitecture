using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;

public sealed class StrongIdValueComparer<TEntity> : ValueComparer<StrongId<TEntity>>
{
    public StrongIdValueComparer() : base(
        (a, b) => a.Value == b.Value,
        id => id.Value.GetHashCode(),
        id => new StrongId<TEntity>(id.Value))
    { }
}
