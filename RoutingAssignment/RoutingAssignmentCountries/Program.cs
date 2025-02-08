var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Dictionary<string, string> countries = new Dictionary<string, string>
{
    {"1", "India"},
    {"2", "USA"},
    {"3", "UK"},
    {"4", "Canada"},
    {"5", "Australia"}
};

app.MapGet("/", () => "Hello World!");
app.MapGet("/countries", () => countries);
app.MapGet("/countries/{id:int:min(1):max(100)}", async (context) =>
{

    string? key = context.Request.RouteValues["id"] as string;
    if (key == null || !countries.ContainsKey(key))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Country not found");
    }
    else
    {
        await context.Response.WriteAsync(countries[key]);
    }
});

app.Run();
