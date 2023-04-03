using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IntusWindows.Sales.Order.Web.Blazor;
using IntusWindows.Sales.Order.Web.Services.ExtensionMethods;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using IntusWindows.Sales.Order.Web.Services.Services;
using IntusWindows.Sales.Order.Web.Services;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClients();

await builder.Build().RunAsync();

