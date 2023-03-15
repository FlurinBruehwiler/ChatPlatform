using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatPlatformBackend.EndpointDefinitions;

public class AuthenticationEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost( "/register", Register);
        app.MapPost( "/mobile/register", MobileRegister);
        app.MapPost( "/login", Login);
        app.MapPost( "/mobile/login", MobileLogin);
        app.MapGet("/logout", Logout);
        app.MapGet("/protected", ProtectedEndpoint).RequireAuthorization();
    }

    private async Task<IResult> Register(DtoAuthUser dtoAuthUser, [FromServices] IUserService userService, HttpResponse httpResponse)
    {
        await userService.RegisterUser(dtoAuthUser, httpResponse);
        return Results.Ok();
    }

    private async Task<IResult> MobileRegister(DtoAuthUser dtoAuthUser, [FromServices] IUserService userService, HttpResponse httpResponse)
    {
        var token = await userService.MobileRegisterUser(dtoAuthUser, httpResponse);
        return Results.Ok(token);
    }
    
    private async Task<IResult> Login(DtoAuthUser dtoUser, [FromServices] IUserService userService, HttpResponse httpResponse)
    {
        await userService.LoginUser(dtoUser, httpResponse);
        return Results.Ok();
    }
    
    private async Task<IResult> MobileLogin(DtoAuthUser dtoUser, [FromServices] IUserService userService, HttpResponse httpResponse)
    {
        var token = await userService.MobileLoginUser(dtoUser, httpResponse);
        return Results.Ok(token);
    }
    
    private IResult Logout(HttpContext context)
    {
        context.Response.Cookies.Append("X-Access-Token", "",  new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(-1)
        });
        return Results.Ok();
    }

    private IResult ProtectedEndpoint()
    {
        return Results.Ok();
    }
}