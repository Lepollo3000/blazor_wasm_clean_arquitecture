using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;

public sealed class StrongIdValueConverter<TEntity> : ValueConverter<StrongId<TEntity>, int>
{
    public StrongIdValueConverter() : base(id => id.Value, value => new StrongId<TEntity>(value)) { }
}
