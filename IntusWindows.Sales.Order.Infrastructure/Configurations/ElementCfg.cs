using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntusWindows.Sales.Order.Infrastructure.Configurations;

internal class ElementCfg : IEntityTypeConfiguration<Element>
{

    public void Configure(EntityTypeBuilder<Element> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.elementType).HasColumnName("elementType");

        builder.Ignore(x => x.Version);


    }
}

