using IntusWindows.Sales.Order.Web.Blazor.Hosted.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IntusWindows.Sales.Order.Web.Services.ExtensionMethods;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using IntusWindows.Sales.Order.Web.Services.Services;
using IntusWindows.Sales.Order.Web.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpProgressBarService();
builder.Services.AddHttpClients();

await builder.Build().RunAsync();
