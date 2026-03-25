namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

/// <summary>
/// Base record para IDs fuertemente tipados. Usa un Guid internamente.
/// <br/>Derivar con: public record UserId(Guid Value) : StrongId<UserId>(Value);
/// </summary>
public abstract record StrongId<TSelf>(int Value)
    where TSelf : StrongId<TSelf>
{
    public static TSelf New() => Create(default);

    public static TSelf Empty => Create(default);

    public static TSelf From(int value) => Create(value);

    public static bool TryParse(string? input, out TSelf? result)
    {
        if (int.TryParse(input, out var guid))
        {
            result = Create(guid);

            return true;
        }

        result = null;

        return false;
    }

    public override string ToString() => Value.ToString();

    // El factory method se resuelve mediante el constructor del tipo derivado.
    private static TSelf Create(int value) =>
        (TSelf)Activator.CreateInstance(typeof(TSelf), value)!;
}
