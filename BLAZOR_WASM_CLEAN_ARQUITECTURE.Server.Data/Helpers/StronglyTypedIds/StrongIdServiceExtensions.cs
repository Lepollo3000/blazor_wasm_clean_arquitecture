using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers.StronglyTypedIds;

/// <summary>
/// Extensiones de IServiceCollection para registrar toda la infraestructura de StrongIds de una vez.
/// </summary>
public static class StrongIdServiceExtensions
{
    /// <summary>
    /// Registra:
    /// <br/>- JsonConverter factory para System.Text.Json
    /// <br/>- TypeConverters para model binding en ASP.NET Core
    /// </summary>
    ///
    /// <remarks>
    /// Llamar en Program.cs antes de builder.Build().
    /// </remarks>
    public static IServiceCollection AddStrongIds(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        // Registrar TypeConverters para todos los StrongIds encontrados
        // en los assemblies indicados (necesario para route/query binding).
        foreach (var assembly in assemblies)
        {
            var strongIdTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && IsStrongId(t));

            foreach (var type in strongIdTypes)
            {
                var converterType = typeof(StrongIdTypeConverter<>).MakeGenericType(type);
                TypeDescriptor.AddAttributes(type, new TypeConverterAttribute(converterType));
            }
        }

        return services;
    }

    /// <summary>
    /// Configura JsonSerializerOptions para incluir el converter factory.
    /// </summary>
    /// 
    /// <remarks>
    /// <b>Uso típico:</b> services.ConfigureHttpJsonOptions(o => o.SerializerOptions.AddStrongIdSupport())
    /// </remarks>
    public static JsonSerializerOptions AddStrongIdSupport(this JsonSerializerOptions options)
    {
        options.Converters.Add(new StrongIdJsonConverterFactory());

        return options;
    }

    private static bool IsStrongId(Type type)
    {
        var current = type.BaseType;

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
}
