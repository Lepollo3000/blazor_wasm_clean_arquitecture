using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds;

public static class StrongIdExtensions
{
    public static void UseStronglyTypedIds(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                var clrType = property.ClrType;

                if (!clrType.IsGenericType) continue;
                if (clrType.GetGenericTypeDefinition() != typeof(StrongId<>)) continue;

                var entityArg = clrType.GetGenericArguments()[0];

                // Construye converter/comparer cerrados por reflexión
                var converterType = typeof(StrongIdValueConverter<>).MakeGenericType(entityArg);
                var comparerType = typeof(StrongIdValueComparer<>).MakeGenericType(entityArg);

                var converter = (ValueConverter)Activator.CreateInstance(converterType)!;
                var comparer = (ValueComparer)Activator.CreateInstance(comparerType)!;

                property.SetValueConverter(converter);
                property.SetValueComparer(comparer);

                property.IsPrimaryKey();
            }
        }
    }
}
