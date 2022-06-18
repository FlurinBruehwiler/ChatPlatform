using ChatPlatformBackend.Exceptions;
using ChatPlatformBackend.Extensions;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.AddServices();

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
        
        if (exceptionHandlerPathFeature?.Error is not BadRequestException exception)
            return;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        
        await context.Response.WriteAsJsonAsync(exception.Error);
    });
});

app.UseEndpointDefinitions();


app.UseAuthentication();
app.UseAuthorization();



app.Run();