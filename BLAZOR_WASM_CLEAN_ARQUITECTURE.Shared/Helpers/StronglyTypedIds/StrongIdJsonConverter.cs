using System.Text.Json;
using System.Text.Json.Serialization;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

public sealed class StrongIdJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(StrongId<>);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var entityType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(IdJsonConverter<>).MakeGenericType(entityType);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private sealed class IdJsonConverter<TEntity> : JsonConverter<StrongId<TEntity>>
    {
        public override StrongId<TEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => new(reader.GetInt32());

        public override void Write(Utf8JsonWriter writer, StrongId<TEntity> value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value.Value);
    }
}
