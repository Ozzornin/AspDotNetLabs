using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspDotNetLab2.Classes
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CounterMiddleware
    {
        private readonly RequestDelegate _next;

        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, Counter counter)
        {
            counter.Increment();
            if (httpContext.Request.Path == "/services/general-counter")
            {
                httpContext.Response.ContentType = "text/html";
                await httpContext.Response.WriteAsync($"Total requests: {counter.GetCount()}");
            }
            else await _next.Invoke(httpContext);
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CounterMiddlewareExtensions
    {
        public static IApplicationBuilder UseCounterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CounterMiddleware>();
        }
    }
}
