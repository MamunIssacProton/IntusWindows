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

        services.AddHttpClient<IDimensionService,DimensionService>();
        services.AddHttpClient<IElementService,ElementService> ();
        services.AddHttpClient<IOrderService, OrderService>();
        services.AddHttpClient<IWindowService, WindowService>();
       
        return services;
    }

    public static IServiceCollection AddHttpProgressBarService(this IServiceCollection services)
    {
       return services.AddScoped<IProgress<int>, Progress<int>>();
    }
   
}