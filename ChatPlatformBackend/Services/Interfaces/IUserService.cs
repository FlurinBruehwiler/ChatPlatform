using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IUserService
{
    public User GetUserByContextWithChats(HubCallerContext context);
    public Task<User> GetUserByContextAsync(HubCallerContext context);
    public Task<User> GetUserByIdAsync(int id);
}