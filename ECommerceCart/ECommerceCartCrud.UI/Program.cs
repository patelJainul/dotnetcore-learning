using ECommerceCartCrud.UI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.Run();
