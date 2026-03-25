using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers.StronglyTypedIds;

/// <summary>
/// Value converter que enseña a EF Core a persistir un StrongId como Guid.
/// </summary>
public class StrongIdValueConverter<TId> : ValueConverter<TId, int>
    where TId : StrongId<TId>
{
    public StrongIdValueConverter()
        : base(
            id => id.Value,
            guid => StrongId<TId>.From(guid))
    { }
}
