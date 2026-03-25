using System.Text.Json;
using System.Text.Json.Serialization;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

/// <summary>
/// JSON converter genérico para cualquier StrongId.
/// <br/>Registrar en el JsonSerializerOptions o via atributo.
/// </summary>
public class StrongIdJsonConverter<TId> : JsonConverter<TId>
    where TId : StrongId<TId>
{
    public override TId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var raw = reader.GetInt32();

        return raw == default ? null : StrongId<TId>.From(raw);
    }

    public override void Write(Utf8JsonWriter writer, TId value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value.Value);
}
