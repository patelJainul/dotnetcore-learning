using ServiceContractors;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// builder.Services.Add(new ServiceDescriptor(typeof(ICitiesWeatherServices), new CitiesWeatherServices()));
builder.Services.AddScoped<ICitiesWeatherServices, CitiesWeatherServices>();
var app = builder.Build();

app.MapControllers();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
