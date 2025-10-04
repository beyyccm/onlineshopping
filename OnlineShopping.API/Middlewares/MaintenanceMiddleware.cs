// Middlewares/MaintenanceMiddleware.cs
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnlineShopping.API.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _isMaintenance;

        public MaintenanceMiddleware(RequestDelegate next, bool isMaintenance)
        {
            _next = next;
            _isMaintenance = isMaintenance;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_isMaintenance)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Site is under maintenance. Please try again later.");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class MaintenanceMiddlewareExtensions
    {
        public static IApplicationBuilder UseMaintenance(this IApplicationBuilder builder, bool isMaintenance)
        {
            return builder.UseMiddleware<MaintenanceMiddleware>(isMaintenance);
        }
    }
}
