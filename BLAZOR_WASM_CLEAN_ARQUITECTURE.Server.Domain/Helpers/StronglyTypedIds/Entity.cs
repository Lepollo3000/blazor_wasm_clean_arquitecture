using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;

/// <summary>
/// Entidad base para entidades con un StrongId como PK.
/// </summary>
public abstract class Entity<TId> where TId : StrongId<TId>
{
    public TId Id { get; protected set; } = null!;

    protected Entity() { }

    protected Entity(TId id) => Id = id;
}
