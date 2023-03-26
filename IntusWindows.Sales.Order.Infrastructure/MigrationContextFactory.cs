using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IntusWindows.Sales.Order.Infrastructure;
public class MigrationContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<Context>();

        builder.UseNpgsql("User ID=user;password=password;Host=localhost;Port=5432;Database=Yo;Pooling=true;Include Error Detail=true;");

        return new Context(builder.Options);
    }
}

