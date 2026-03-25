using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.AspNetCore.Identity;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;

/// <summary>
/// IdentityRole genérico con StrongId.
/// </summary>
///
/// <remarks>
/// <b>IMPORTANTE:</b>
/// <br/>Identity serializa la PK como string internamente.
/// <br/>Por eso sobrescribimos Id con el tipo Guid subyacente y exponemos el StrongId en una propiedad separada que EF mapea al mismo campo.
/// </remarks>
public class StrongIdentityRole<TRoleId> : IdentityRole<int>
    where TRoleId : StrongId<TRoleId>
{
    /// <summary>
    /// Propiedad tipada. EF la mapea a la misma columna que IdentityUser.Id
    /// mediante la configuración en el DbContext.
    /// </summary>
    public TRoleId TypedId
    {
        get => StrongId<TRoleId>.From(Id);
        private set => Id = value.Value;
    }

    protected StrongIdentityRole() { }

    protected StrongIdentityRole(TRoleId id, string name)
    {
        Id = id.Value;
        Name = name;
        NormalizedName = name.ToUpperInvariant(); // Sobreescribir en el tipo concreto
    }
}
