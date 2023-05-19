using AspDotNetLab3.Loggers;
using AspDotNetLab3.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Text;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("conf.json");

builder.Services.AddMvc(options =>
    options.EnableEndpointRouting = false);
builder.Services.AddSession()
    .AddSingleton<ILoggerProvider, FileLoggerProvider>()
    .AddHttpContextAccessor()
    .AddLogging();

var app = builder.Build();
//app.UseMiddleware<TitleMiddleware>(); 

app.UseMiddleware<LoggerMiddleware>();

app.UseSession();


app.MapGet("/{lang}/{controller}/{action}/{id?:int}", (string language, string controller, string action, int? id) =>
{    
    var sb = new StringBuilder();
    sb.AppendLine($"Controller: {controller}");
    sb.AppendLine($"Action: {action}");
    if (id != null)
        sb.AppendLine($"Id: {id}");
    sb.AppendLine($"Language: {language}");
    return sb.ToString();
});
app.MapGet("/{controller}/{action}/{id?:int}", (string controller, string action, int? id) =>
{
    var sb = new StringBuilder();
    sb.AppendLine($"Controller: {controller}");
    sb.AppendLine($"Action: {action}");
    if (id != null)
        sb.AppendLine($"Id: {id}");
    return sb.ToString();
});

app.MapGet("/session/add/{key}/{value}", async (HttpContext context, string key, string value) =>
{
    context.Session.SetString(key, value);
    await context.Response.WriteAsync($"Session key: {key} is avaiable with value {context.Session.GetString(key)}");
});
app.MapGet("/cookie/add/{key}/{value}", async (HttpContext context, string key, string value) =>
{
    context.Response.Cookies.Append(key, value);
    await context.Response.WriteAsync($"Cookie key: {key} is avaiable with value {value}");
});
app.MapGet("/session/view/{key}", async (HttpContext context, string key) =>
{
    string? value = context.Session.GetString(key);
    await context.Response.WriteAsync($"{key} = {value}");
});
app.MapGet("/cookie/view/{key}", async (HttpContext context, string key) =>
{
    object? value = context.Request.Cookies[key];
    await context.Response.WriteAsync($"{key} - {value}");
});



app.Map("/hello", builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("SEKA");
    });
});

app.Run();
