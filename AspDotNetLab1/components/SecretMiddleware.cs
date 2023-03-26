using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspDotNetLab1.components
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecretMiddleware
    {
        private readonly RequestDelegate _next;

        public SecretMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if(httpContext.Request.Path == "/secret-571743235872348")
            {
                httpContext.Response.ContentType = "text/html";
                await httpContext.Response.SendFileAsync("static/files/secret.html");
            }      
            else await _next.Invoke(httpContext);            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecretMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecretMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecretMiddleware>();
        }
    }
}
