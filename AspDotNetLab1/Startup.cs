using AspDotNetLab1.components;
using Microsoft.AspNetCore.Builder;

namespace AspDotNetLab1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<LoggerMiddleware>();
            services.AddScoped<SecretMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add your middleware pipeline here
        }
    }
}
