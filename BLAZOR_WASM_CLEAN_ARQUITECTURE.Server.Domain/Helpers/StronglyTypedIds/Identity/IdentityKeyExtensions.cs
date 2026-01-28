using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Server.Domain.Helpers.StronglyTypedIds.Identity;

public static class IdentityKeyExtensions
{
    private sealed class IdentityKeyConverter : ValueConverter<IdentityKey, int>
    {
        public IdentityKeyConverter() : base(k => k.Value, v => new IdentityKey(v)) { }
    }

    private sealed class IdentityKeyComparer : ValueComparer<IdentityKey>
    {
        public IdentityKeyComparer() : base((a, b) => a.Value == b.Value, k => k.Value.GetHashCode(), k => new IdentityKey(k.Value)) { }
    }

    public static void UseIdentityKeyAsInt(this ModelBuilder modelBuilder)
    {
        var converter = new IdentityKeyConverter();
        var comparer = new IdentityKeyComparer();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var prop in entityType.GetProperties())
            {
                if (prop.ClrType == typeof(IdentityKey))
                {
                    prop.SetValueConverter(converter);
                    prop.SetValueComparer(comparer);
                    prop.SetColumnType("int");
                }
            }
        }
    }
}
