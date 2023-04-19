using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;
namespace IntusWindows.Sales.Order.Web.Blazor;

public static class CssUtilsExtension
{
    public static async Task AddClass(this ElementReference elementRef, IJSRuntime jsRuntime, string className)
    {
        await jsRuntime.InvokeVoidAsync("addClass", elementRef, className);
    }

    public static async Task RemoveClass(this ElementReference elementRef, IJSRuntime jsRuntime, string className)
    {
        await jsRuntime.InvokeVoidAsync("removeClass", elementRef, className);
    }
}

