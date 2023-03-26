using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Element = IntusWindows.Sales.Order.Domain.Entities.Element;

namespace IntusWindows.Sales.Order.Infrastructure;

public class Context : DbContext
{
    public DbSet<Element> Elements { get; set; }

    public DbSet<Dimension> Dimensions { get; set; }

    public DbSet<Window> Windows { get; set; }

    public Context() => this.Database.Migrate();

    public Context(DbContextOptions<Context> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("User ID=user;password=password;Server=localhost;Port=5432;Database=db;Pooling=true;Include Error Detail=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DimensionCfg());
        modelBuilder.ApplyConfiguration(new ElementCfg());
        modelBuilder.ApplyConfiguration(new WindowCfg());

    }
}

