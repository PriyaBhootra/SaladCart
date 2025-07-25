using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
namespace SaladCart.Models
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(RequestDelegate requestDelegate,ILogger<RequestLoggingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Console.WriteLine($"[Request]{context.Request.Method},{context.Request.Path}");
            // await _requestDelegate(context);
            // Console.WriteLine($"[Response]{context.Response.StatusCode}");
            _logger.LogInformation("[Request] {Method}, {Path}", context.Request.Method, context.Request.Path);
            await _requestDelegate(context);
            _logger.LogInformation("[Response] {StatusCode}", context.Response.StatusCode);
        }

       

    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingMiddleware(IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
