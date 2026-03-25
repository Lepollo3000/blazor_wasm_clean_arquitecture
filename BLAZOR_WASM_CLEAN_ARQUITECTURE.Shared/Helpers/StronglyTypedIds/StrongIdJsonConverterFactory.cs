using System.Text.Json;
using System.Text.Json.Serialization;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

/// <summary>
/// Factory de JsonConverters que detecta en tiempo de ejecución cualquier StrongId y devuelve el converter correcto.
/// </summary>
/// 
/// <remarks>
/// Registrar <b>una sola vez</b> — cubre todos los tipos actuales y futuros.
/// </remarks>
public class StrongIdJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        var current = typeToConvert;
        while (current != null && current != typeof(object))
        {
            if (current.IsGenericType
            && current.GetGenericTypeDefinition() == typeof(StrongId<>))
            {
                return true;
            }

            current = current.BaseType;
        }
        return false;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(StrongIdJsonConverter<>).MakeGenericType(typeToConvert);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}
