namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

/// <summary>
/// Conversor genérico para que ASP.NET Core pueda bindear StrongIds desde la URL/query string.
/// <br/>Registrar en los options del MVC/Minimal API.
/// </summary>
public class StrongIdTypeConverter<TId> : System.ComponentModel.TypeConverter
    where TId : StrongId<TId>
{
    public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, Type sourceType) =>
        sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context,
        System.Globalization.CultureInfo? culture, object value)
    {
        if (value is string s && int.TryParse(s, out var intValue))
            return StrongId<TId>.From(intValue);

        return base.ConvertFrom(context, culture, value);
    }
}
