namespace AspDotNetLab2.Classes
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;

        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext , ITimerService timerService)
        {

            if (httpContext.Request.Path == "/services/time")
            {
                httpContext.Response.ContentType = "text/html";
                await httpContext.Response.WriteAsync(timerService.GetDate());
            }
            else await _next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TimerMiddlewareExtensions
    {
        public static IApplicationBuilder UseTimerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimerMiddleware>();
        }
    }
}
