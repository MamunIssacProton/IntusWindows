using System;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntusWindows.Sales.Order.Infrastructure.Configurations;

public class WindowCfg : IEntityTypeConfiguration<Window>
{
    public void Configure(EntityTypeBuilder<Window> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.windowName)
                .HasConversion(windowName => windowName.Value, Value => WindowName.Create(Value))
                .HasMaxLength(20);


        builder.HasMany<Element>(x => x.SubElements).WithMany();

        builder.Property(x => x.TotalSubElements).HasColumnName("totalSubElements").HasMaxLength(4);

        builder.Property(x => x.QuantityOfWindows).HasColumnName("quantityOfWindows").HasMaxLength(4);

        builder.Ignore(x => x.Version);

    }
}

