using IntusWindows.Sales.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntusWindows.Sales.Order.Infrastructure.Configurations;

public class StateCfg:IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("name");

        builder.Ignore(x => x.Version);
       
    }
}

