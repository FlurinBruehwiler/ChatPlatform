using ChatPlatformBackend.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ChatPlatformBackend.EndpointDefinitions;

public class ExceptionHandlerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
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
    }
}