using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatPlatformBackend.EndpointDefinitions;

public class AuthenticationEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost( "/register", Register);
        app.MapPost( "/login", Login);
    }

    private async Task<IResult> Register(DtoUser dtoUser, [FromServices] IUserService userService, HttpResponse httpResponse)
    {
        await userService.RegisterUser(dtoUser, httpResponse);
        return Results.Ok();
    }
    
    private async Task<IResult> Login(DtoUser dtoUser, [FromServices] IUserService userService, HttpResponse httpResponse)
    {
        await userService.LoginUser(dtoUser, httpResponse);
        return Results.Ok();
    }
}