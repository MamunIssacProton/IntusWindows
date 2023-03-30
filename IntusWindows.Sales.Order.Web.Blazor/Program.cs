using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IntusWindows.Sales.Order.Web.Blazor;
using IntusWindows.Sales.Order.Web.Services.ExtensionMethods;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using IntusWindows.Sales.Order.Web.Services.Services;
using IntusWindows.Sales.Order.Web.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.Services.AddHttpClient<IDimensionService, DimensionService>(client =>
//{
//    client.BaseAddress = new Uri(ApiEndpoints.Order);
//    client.DefaultRequestHeaders.Add("accept", "text/plain");
//});
builder.Services.AddHttpClients();
//builder.Services.AddHttpClient();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

