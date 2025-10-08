using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace OnlineShopping.API.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;


        public MaintenanceMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var maintenance = _config.GetValue<bool>("MaintenanceMode");
            if (maintenance)
            {
                context.Response.StatusCode = 503;
                await context.Response.WriteAsync("Site bakýmda. Daha sonra tekrar deneyin.");
                return;
            }


            await _next(context);
        }
    }
}