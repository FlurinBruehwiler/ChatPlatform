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
        if (context.User?.Identity is null)
            throw new BadRequestException(Errors.NoAuth);

        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == context.UserIdentifier);

        if (user is null)
            throw new BadRequestException(Errors.UserNotFound);

        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        if (user is null)
            throw new BadRequestException(Errors.WrongPassword);
        return user;
    }

    public async Task<User?> TryGetUserByUsernameAsync(string username)
    {
        return await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task RegisterUser(DtoAuthUser dtoAuthUser, HttpResponse httpResponse)
    {
        var user = await CreateUser(dtoAuthUser);

        _authService.AppendAccessToken(httpResponse, user);
    }

    private async Task<User> CreateUser(DtoAuthUser dtoAuthUser)
    {
        if (string.IsNullOrWhiteSpace(dtoAuthUser.Username))
            throw new BadRequestException(Errors.UsernameToShort);

        if (string.IsNullOrWhiteSpace(dtoAuthUser.Password))
            throw new BadRequestException(Errors.PasswordToWeak);

        if (await _chatAppContext.Users.AnyAsync(x => x.Username == dtoAuthUser.Username))
            throw new BadRequestException(Errors.UsernameAlreadyExists);
        
        _authService.CreatePasswordHash(dtoAuthUser.Password, out var passwordHash, out var passwordSalt);
        var user = new User
        {
            Username = dtoAuthUser.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        _chatAppContext.Users.Add(user);
        await _chatAppContext.SaveChangesAsync();

        return user;
    }

    public async Task<string> MobileRegisterUser(DtoAuthUser dtoUser, HttpResponse httpResponse)
    {
        var user = await CreateUser(dtoUser);

        return _authService.CreateToken(user);
    }

    public async Task LoginUser(DtoAuthUser dtoAuthUser, HttpResponse httpResponse)
    {
        var user = await GetUserByUsernameAsync(dtoAuthUser.Username);

        if (!_authService.VerifyPasswordHash(dtoAuthUser.Password, user.PasswordHash, user.PasswordSalt))
            throw new BadRequestException(Errors.WrongPassword);

        _authService.AppendAccessToken(httpResponse, user);
    }

    public async Task<string> MobileLoginUser(DtoAuthUser dtoAuthUser, HttpResponse httpResponse)
    {
        var user = await GetUserByUsernameAsync(dtoAuthUser.Username);

        if (!_authService.VerifyPasswordHash(dtoAuthUser.Password, user.PasswordHash, user.PasswordSalt))
            throw new BadRequestException(Errors.WrongPassword);

        return _authService.CreateToken(user);
    }
}