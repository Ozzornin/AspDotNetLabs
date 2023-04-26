using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspDotNetLab2.Classes
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RandomMiddleware
    {
        private readonly RequestDelegate _next;

        public RandomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, RandomService randomService, IRandom random)
        {
            if (httpContext.Request.Path == "/services/random")
            {
                httpContext.Response.ContentType = "text/html";
                await httpContext.Response.WriteAsync($"Service:{randomService.Service.GetRandom()} " +
                    $"Random:{random.GetRandom()}");
            }
            else await _next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RandomMiddlewareExtensions
    {
        public static IApplicationBuilder UseRandomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RandomMiddleware>();
        }
    }
}
