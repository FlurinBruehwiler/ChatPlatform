using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IUserService
{
    public User GetUserByContextWithChats(HubCallerContext context);
    public User GetUserByContext(HubCallerContext context);
    public User GetUserById(int id);
}