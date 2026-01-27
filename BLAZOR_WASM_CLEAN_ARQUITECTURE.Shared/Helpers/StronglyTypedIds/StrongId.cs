namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;


public readonly record struct StrongId<TEntity>(int Value)
{
    public static StrongId<TEntity> New() => default;
    public static StrongId<TEntity> New(int value) => new(value);

    public override string ToString() => Value.ToString();

    // Para Minimal APIs / binding: patrón TryParse
    public static bool TryParse(string? value, IFormatProvider? provider, out StrongId<TEntity> result)
    {
        if (int.TryParse(value, out var intValue))
        {
            result = new StrongId<TEntity>(intValue);

            return true;
        }

        result = default;

        return false;
    }
}

