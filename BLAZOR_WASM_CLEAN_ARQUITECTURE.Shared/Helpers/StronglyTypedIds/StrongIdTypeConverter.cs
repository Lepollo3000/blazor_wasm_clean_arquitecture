using System.ComponentModel;
using System.Globalization;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

public class StrongIdInt32Converter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string s && int.TryParse(s, NumberStyles.Integer, culture ?? CultureInfo.InvariantCulture, out var i))
            return new StrongId<int>(i);

        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is StrongId<int> key)
            return key.Value.ToString(culture ?? CultureInfo.InvariantCulture);

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
