using AspDotNetLab2.Classes;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITimerService, TimerService>();
builder.Services.AddScoped<RandomService>();
builder.Services.AddScoped<IRandom, RandomNumber>();
builder.Services.AddSingleton<Counter>();


var app = builder.Build();
app.UseCounterMiddleware();
IServiceCollection _services = builder.Services;
app.Map("/services/list", builder =>
{
    builder.Run(async context =>
    {
        var sb = new StringBuilder();
        sb.Append("<h1>Óñ³ ñåðâ³ñè</h1>");
        sb.Append("<table border=\"1\">");
        sb.Append("<tr><th>Òèï</th><th>Lifetime</th><th>Ðåàë³çàö³ÿ</th></tr>");
        foreach (var svc in _services)
        {
            sb.Append("<tr>");
            sb.Append($"<td>{svc.ServiceType.FullName}</td>");
            sb.Append($"<td>{svc.Lifetime}</td>");
            sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync(sb.ToString());
    });
});
app.UseTimerMiddleware();
app.UseRandomMiddleware();

app.Run();