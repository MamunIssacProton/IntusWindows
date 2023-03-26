using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Infrastructure.ExtensionMethods;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ApplicationService>();
builder.Services.AddDataRepositories(builder.Configuration.GetConnectionString("postgres"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
