var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();

app.MapControllerRoute(name: "default", pattern: "{controllerName=Home}/{action=Index}");

app.Run();
