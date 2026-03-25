using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;

/// <summary>
/// Entidad base con auditoría básica.
/// </summary>
public abstract class AuditableEntity<TId> : Entity<TId>
    where TId : StrongId<TId>
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }

    protected AuditableEntity() { }
    protected AuditableEntity(TId id) : base(id) { }
}
