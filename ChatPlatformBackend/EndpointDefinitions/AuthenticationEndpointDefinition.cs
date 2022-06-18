using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
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

    private async Task<IResult> Register(DtoUser dtoUser, [FromServices] IAuthService authService, [FromServices]ChatAppContext chatAppContext, HttpResponse httpResponse)
    {
        authService.CreatePasswordHash(dtoUser.Password, out var passwordHash, out var passwordSalt);
        var user = new User
        {
            Username = dtoUser.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        chatAppContext.Users.Add(user);
        await chatAppContext.SaveChangesAsync();
        authService.AppendAccessToken(httpResponse, user);

        return Results.Ok(user.UserId);
    }
    
    private async Task<IResult> Login(DtoUser dtoUser, [FromServices]IAuthService authService,[FromServices] IUserService userService, HttpResponse httpResponse)
    {
        var user = await userService.GetUserByUsernameAsync(dtoUser.Username);

        if (!authService.VerifyPasswordHash(dtoUser.Password, user.PasswordHash, user.PasswordSalt))
            return Results.BadRequest("Wrong Password");
    
        authService.AppendAccessToken(httpResponse, user);
        
        return Results.Ok();
    }
}