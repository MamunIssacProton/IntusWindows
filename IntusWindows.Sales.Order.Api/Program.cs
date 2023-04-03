using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Infrastructure.ExtensionMethods;
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
                                     .AllowAnyMethod()
            ); ;
});

var app = builder.Build();
app.UseCors("webapp");
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
