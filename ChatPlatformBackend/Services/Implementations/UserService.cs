using System.Security.Claims;
using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Exceptions;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.Services.Implementations;

public class UserService : IUserService
{
    private readonly ChatAppContext _chatAppContext;
    private readonly IAuthService _authService;

    public UserService(ChatAppContext chatAppContext, IAuthService authService)
    {
        _chatAppContext = chatAppContext;
        _authService = authService;
    }
    
    public User GetUserByContextWithChats(HubCallerContext context)
    {
        if (context.User?.Identity is null)
            throw new BadRequestException(Errors.NoAuth);
        
        var user = _chatAppContext.Users.Where(x => x.Username == context.UserIdentifier)
            .Include(x => x.Chats).FirstOrDefault();

        if (user is null)
            throw new BadRequestException(Errors.UserNotFound);

        return user;
    }

    public async Task<User> GetUserByContextAsync(HubCallerContext context)
    {
        if(context.User?.Identity is null)
            throw new BadRequestException(Errors.NoAuth);
        
        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == context.UserIdentifier); 
        
        if(user is null)
            throw new BadRequestException(Errors.UserNotFound);

        return user;
    }
    
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        if(user is null)
            throw new BadRequestException(Errors.UserNotFound);
        return user;
    }

    public async Task RegisterUser(DtoUser dtoUser, HttpResponse httpResponse)
    {
        if (await _chatAppContext.Users.AnyAsync(x => x.Username == dtoUser.Username))
            throw new BadRequestException(Errors.UsernameAlreadyExists);
        
        _authService.CreatePasswordHash(dtoUser.Password, out var passwordHash, out var passwordSalt);
        var user = new User
        {
            Username = dtoUser.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        _chatAppContext.Users.Add(user);
        await _chatAppContext.SaveChangesAsync();
        _authService.AppendAccessToken(httpResponse, user);
    }

    public async Task LoginUser(DtoUser dtoUser, HttpResponse httpResponse)
    {
        var user = await GetUserByUsernameAsync(dtoUser.Username);

        if (!_authService.VerifyPasswordHash(dtoUser.Password, user.PasswordHash, user.PasswordSalt))
            throw new BadRequestException(Errors.WrongPassword);
    
        _authService.AppendAccessToken(httpResponse, user);
    }
}