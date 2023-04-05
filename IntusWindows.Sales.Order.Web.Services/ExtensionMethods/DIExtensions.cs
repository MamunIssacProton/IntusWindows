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
        services.AddTransient<IDimensionService,DimensionService>();
        services.AddTransient<IElementService,ElementService> ();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IWindowService, WindowService>();
        services.AddTransient<IStateService, StateService>();
        return services;
    }

    public static IServiceCollection AddHttpProgressBarService(this IServiceCollection services)
    {

        services.AddScoped<IProgress<long>, Progress<long>>();
        services.AddScoped<ProgressService>();
        services.AddTransient<ProgressiveHttpClient>();
      
        return services;
    }
   
}