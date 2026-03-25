using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.AspNetCore.Identity;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;

/// <summary>
/// IdentityUser genérico que usa un StrongId como TKey.
/// <br/>Identity requiere que TKey sea IEquatable, lo cual record satisface.
/// </summary>
/// 
/// <remarks>
/// <b>IMPORTANTE:</b>
/// <br/>Identity serializa la PK como string internamente.
/// <br/>Por eso sobrescribimos Id con el tipo Guid subyacente y exponemos el StrongId en una propiedad separada que EF mapea al mismo campo.
/// </remarks>
public class StrongIdentityUser<TUserId> : IdentityUser<int>
    where TUserId : StrongId<TUserId>
{
    /// <summary>
    /// Propiedad tipada. EF la mapea a la misma columna que IdentityUser.Id
    /// mediante la configuración en el DbContext.
    /// </summary>
    public TUserId TypedId
    {
        get => StrongId<TUserId>.From(Id);
        private set => Id = value.Value;
    }

    protected StrongIdentityUser() { }

    protected StrongIdentityUser(TUserId id)
    {
        Id = id.Value;
        UserName = id.Value.ToString(); // Sobreescribir en el tipo concreto
    }
}
