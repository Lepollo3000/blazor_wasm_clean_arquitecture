using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Data.Helpers.StronglyTypedIds;

/// <summary>
/// Extensiones de ModelBuilder para aplicar los conversores de forma global o por propiedad sin repetir código.
/// </summary>
public static class StrongIdModelBuilderExtensions
{
    /// <summary>
    /// Excluye del modelo todos los tipos que deriven de StrongId para que EF Core no los trate como entidades.
    /// </summary>
    /// 
    /// <remarks>
    /// Llamar AL INICIO de OnModelCreating, antes de cualquier otra configuración.
    /// </remarks>
    public static ModelBuilder IgnoreStrongIds(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var strongIdTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && IsStrongId(t, out _));

            foreach (var type in strongIdTypes)
            {
                modelBuilder.Ignore(type);
            }
        }

        return modelBuilder;
    }

    /// <summary>
    /// Aplica automáticamente StrongIdValueConverter a TODAS las propiedades cuyo tipo derive de StrongId en el modelo completo.
    /// </summary>
    /// 
    /// <remarks>
    /// Llamar al final de OnModelCreating.
    /// </remarks>
    public static ModelBuilder ApplyStrongIdConversions(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                var clrType = property.ClrType;

                if (!IsStrongId(clrType, out var converterType))
                {
                    continue;
                }

                var converter = (ValueConverter)Activator.CreateInstance(converterType!)!;

                property.SetValueConverter(converter);
            }
        }

        return modelBuilder;
    }

    /// <summary>
    /// Configura una propiedad concreta como PK de tipo StrongId.
    /// </summary>
    /// 
    /// <remarks>
    /// <b>Uso:</b> builder.HasStrongId(x => x.Id)
    /// </remarks>
    public static PropertyBuilder<TId> HasStrongId<TEntity, TId>(
        this EntityTypeBuilder<TEntity> entity,
        System.Linq.Expressions.Expression<Func<TEntity, TId>> propertyExpression)
        where TEntity : class
        where TId : StrongId<TId>
    {
        return entity
            .Property(propertyExpression)
            .HasConversion(new StrongIdValueConverter<TId>())
            .ValueGeneratedNever(); // El ID se genera en la aplicación, no en la BD
    }

    private static bool IsStrongId(Type type, out Type? converterType)
    {
        var current = type;

        while (current != null && current != typeof(object))
        {
            if (current.IsGenericType
            && current.GetGenericTypeDefinition() == typeof(StrongId<>))
            {
                converterType = typeof(StrongIdValueConverter<>).MakeGenericType(type);

                return true;
            }

            current = current.BaseType;
        }

        converterType = null;

        return false;
    }
}
