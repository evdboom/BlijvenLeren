using BlijvenLerenApi;
using BlijvenLerenApi.Exceptions;
using BlijvenLerenApi.UseCases.Shared;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

var builder = WebApplication.CreateSlimBuilder(args);

var cfg = builder.Configuration
    .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment() && Debugger.IsAttached)
{
    cfg.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}

var config = cfg.Build();
var keyvault = config["KEYVAULT_NAME"];
//if (!string.IsNullOrEmpty(keyvault))
//{
//    cfg.AddAzureKeyVault(new Uri($"https://{keyvault}.vault.azure.net/"), new DefaultAzureCredential());
//}

builder.Services
    .RegisterApplication(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized access.");
        }
        else if (exceptionHandlerPathFeature?.Error is ForbiddenException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("The requested action is not allowed.");
        }
    });
});

var endpoints = app.Services.CreateScope().ServiceProvider.GetServices<IEndpoint>();
var groups = endpoints.GroupBy(endpoint => endpoint.GroupName);
foreach (var group in groups)
{
    var groupBuilder = app.MapGroup(group.Key);
    foreach (var endpoint in group)
    {
        endpoint.RegisterEndpoint(groupBuilder);
    }
}

app.Run();
