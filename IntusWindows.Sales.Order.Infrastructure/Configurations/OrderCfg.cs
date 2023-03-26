using System;
using IntusWindows.Sales.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordr = IntusWindows.Sales.Order.Domain.Entities.Order;
namespace IntusWindows.Sales.Order.Infrastructure.Configurations;

public class OrderCfg:IEntityTypeConfiguration<Ordr>
{
    public void Configure(EntityTypeBuilder<Ordr> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.OrderName).Property(x => x.Value).HasColumnName("orderName");

        builder.Property(x => x.State).HasColumnName("state");
        
        builder.HasMany(x => x.Windows).WithMany();

        builder.Ignore(x => x.Version);
    }
}

