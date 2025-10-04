// Middlewares/RequestLoggingMiddleware.cs
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace OnlineShopping.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User.Identity?.IsAuthenticated == true
                ? context.User.Identity.Name
                : "Anonymous";

            Console.WriteLine($"[{DateTime.Now}] Request to {context.Request.Method} {context.Request.Path} by {user}");

            await _next(context);
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
