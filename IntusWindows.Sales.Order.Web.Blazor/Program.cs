using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IntusWindows.Sales.Order.Web.Blazor;
using IntusWindows.Sales.Order.Web.Services.ExtensionMethods;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using IntusWindows.Sales.Order.Web.Services.Services;
using IntusWindows.Sales.Order.Web.Services;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.JSInterop;
using IntusWindows.Sales.Order.Web.Services.Hubs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpProgressBarService();
builder.Services.AddHttpClients();
builder.Services.AddSignalrService();
await builder.Build().RunAsync();

