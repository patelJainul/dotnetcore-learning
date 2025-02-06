using System;

namespace MiddlewaresExample;

public class GreetingMiddleware(RequestDelegate next) {
    public Task Invoke(HttpContext context) {
        context.Response.WriteAsync($"Hello, {context.Request.Query["name"]}!\n");
        return next(context);
    }

}

public static class GreetingMiddlewareExtension {
    public static IApplicationBuilder UseGreetingMiddleware(this IApplicationBuilder app) {
        return app.UseMiddleware<GreetingMiddleware>();
    }
}