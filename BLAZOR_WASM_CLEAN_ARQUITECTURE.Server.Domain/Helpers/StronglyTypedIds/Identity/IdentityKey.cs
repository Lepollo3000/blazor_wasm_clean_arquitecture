using System.ComponentModel;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;


[TypeConverter(typeof(IdentityKeyTypeConverter))]
public readonly record struct IdentityKey(int Value) : IEquatable<IdentityKey>
{
    public bool IsEmpty => Value <= 0;

    public static bool TryParse(string? s, IFormatProvider? provider, out IdentityKey result)
    {
        if (int.TryParse(s, out var v)) { result = new IdentityKey(v); return true; }
        result = default; return false;
    }

    public override string ToString() => Value.ToString();
}

