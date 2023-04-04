using System;
using System.Reflection;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using IntusWindows.Sales.Order.Web.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace IntusWindows.Sales.Order.Web.Services.ExtensionMethods;

public static class DIExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IBaseService, BaseService>()
              .SetHandlerLifetime(TimeSpan.FromMinutes(3));
        services.AddScoped<IDimensionService,DimensionService>();
        services.AddScoped<IElementService,ElementService> ();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IWindowService, WindowService>();
       
        return services;
    }

    public static IServiceCollection AddHttpProgressBarService(this IServiceCollection services)
    {

        services.AddScoped<IProgress<long>, Progress<long>>();
        services.AddScoped<ProgressService>();
        services.AddScoped<ProgressiveHttpClient>();
      
        return services;
    }
   
}