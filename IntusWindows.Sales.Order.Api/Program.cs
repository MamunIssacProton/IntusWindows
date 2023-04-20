using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Infrastructure.ExtensionMethods;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<ApplicationService>();
builder.Services.AddDataRepositories(builder.Configuration.GetConnectionString("postgres"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "webapp",
                     policy => policy.AllowAnyOrigin()
                                     .AllowAnyHeader()
                                     .AllowAnyMethod());;
});

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("postgres"));
var app = builder.Build();
app.UseCors("webapp");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
