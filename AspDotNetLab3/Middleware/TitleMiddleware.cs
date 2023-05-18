using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspDotNetLab3.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TitleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TitleMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(_configuration["Title"]);            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TitleMiddlewareExtensions
    {
        public static IApplicationBuilder UseTitleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TitleMiddleware>();
        }
    }
}
