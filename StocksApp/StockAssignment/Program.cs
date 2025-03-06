using Microsoft.Extensions.Options;
using Models.Configurations;
using ServiceContractors;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

// builder.Services.AddHttpClient<IFinnHubServices, FinnHubServices>(
//   "finnhub",
//   (serviceProvider, client) =>
//   {
//     var settings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
//     client.BaseAddress = new Uri($"{settings.BaseUrl}");
//     client.DefaultRequestHeaders.Add("X-Finnhub-Token", settings.ApiKey);
//   }
// );

builder.Services.AddHttpClient(
  "finnhub",
  (serviceProvider, client) =>
  {
    var settings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri($"{settings.BaseUrl}");
    client.DefaultRequestHeaders.Add("X-Finnhub-Token", settings.ApiKey);
  }
);

builder.Services.AddScoped<IFinnHubServices, FinnHubServices>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
