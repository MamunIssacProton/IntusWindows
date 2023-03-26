using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntusWindows.Sales.Order.Infrastructure.Configurations;

public class DimensionCfg : IEntityTypeConfiguration<Dimension>
{
    public void Configure(EntityTypeBuilder<Dimension> builder)
    {

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Height).Property(x => x.Value).HasColumnName("height").HasMaxLength(4);

        builder.OwnsOne(x => x.Width).Property(x => x.Value).HasColumnName("width").HasMaxLength(4);

        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(20);

        builder.Ignore(x => x.Version);

        builder.Ignore(x => x.elementType);
    }
}

