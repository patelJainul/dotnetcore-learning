using System;

namespace MiddlewaresExample.CustomMiddleware;

public class MyCustomMiddleware : IMiddleware {
    public Task InvokeAsync(HttpContext context, RequestDelegate next) {
        context.Response.WriteAsync("Hello from the custom ! \n");
        return next(context);
    }
}

public static class CustomMiddlewareExtension {
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app) {
        return app.UseMiddleware<MyCustomMiddleware>();
    }
}
