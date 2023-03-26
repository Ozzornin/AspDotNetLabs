using AspDotNetLab1.components;
using Microsoft.AspNetCore;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel.Design.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(
    Path.Combine(Directory.GetCurrentDirectory(), @"static"))
});
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == 404)
    {
        response.ContentType = "text/html";
        await response.SendFileAsync("static/files/404.html");
    }
});
app.Map("/home/index", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync("static/files/ind.html");
    });
});
app.Map("/home/about", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync("static/files/page.html");
    });
});

app.UseSecretMiddleware();
app.UseLoggerMiddleware();

app.MapGet("/", () => "Hello World!");
app.Run();
