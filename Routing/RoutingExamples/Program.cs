var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => {
    _ = endpoints.MapPost("static", async context => await context.Response.WriteAsync("test!"));
    _ = endpoints.MapGet("file/{filename}.{ext:minlength(3)?}", async context => {
        string? fileName = context.Request.RouteValues["filename"] as string;
        string? ext = context.Request.RouteValues["ext"] as string;
        await context.Response.WriteAsync($"Filename: {fileName}, Extension: {ext}");
    });
});

app.Run(async (context) => await context.Response.WriteAsync("Hello World!"));

app.Run();
