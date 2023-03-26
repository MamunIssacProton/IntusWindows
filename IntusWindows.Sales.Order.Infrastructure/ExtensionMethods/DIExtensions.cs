using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using IntusWindows.Sales.Order.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
namespace IntusWindows.Sales.Order.Infrastructure.ExtensionMethods;

public static class DIExtensions
{
    public static IServiceCollection AddDataRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<Context>(builder => builder.UseNpgsql(connectionString));
        var assembly = Assembly.GetExecutingAssembly();

        var baseInterface = typeof(IBaseRepository);
        var baseContextInterface = typeof(IBaseContextRepository);

        foreach (var t in assembly.GetTypes())
        {
            if (t.IsInterface)
                continue;
            if (t == typeof(BaseContextRepository))
            {
                services.AddScoped<IBaseContextRepository, BaseContextRepository>();
                Console.WriteLine($"added service : {t.Name}");
                continue;
            }
            if (!baseInterface.IsAssignableFrom(t))
                continue;

            var @interface = t.GetInterfaces().FirstOrDefault(x => x != baseInterface && x != baseContextInterface);
            if (@interface == null)
                throw new Exception($"Repository {t.Name} does not have an interface defined");
            services.AddScoped(@interface, t);
            Console.WriteLine($"scoped service added : {@interface.Name} on {t.Name}");
        }

        return services;
    }

    public static void EnsureElementDbCreated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<Context>();
        context.Database.EnsureCreated();
        context.Database.CloseConnection();
    }
}

