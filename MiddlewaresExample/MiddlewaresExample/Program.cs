using MiddlewaresExample;
using MiddlewaresExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();

var app = builder.Build();

app.UseMyCustomMiddleware();

app.UseWhen(
    context => context.Request.Query.ContainsKey("name"),
    app => app.UseGreetingMiddleware()
);

// Middleware to write a message to the response
app.Use(async (context, next) => {
    await context.Response.WriteAsync("Hello from the first! \n");
    await next(context);
});

app.Run();
