using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IUserService
{
    public User GetUserByContextWithChats(HubCallerContext context);
    public Task<User> GetUserByContextAsync(HubCallerContext context);
    public Task<User> GetUserByUsernameAsync(string username);
    public Task RegisterUser(DtoUser dtoUser, HttpResponse httpResponse);
    public Task LoginUser(DtoUser dtoUser, HttpResponse httpResponse);
}