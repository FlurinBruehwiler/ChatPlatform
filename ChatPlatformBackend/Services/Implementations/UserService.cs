using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Implementations;

public class UserService : IUserService
{
    public User GetUserByContextWithChats(HubCallerContext context)
    {
        throw new NotImplementedException();
    }

    public User GetUserByContext(HubCallerContext context)
    {
        throw new NotImplementedException();
    }

    public User GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public string GetDecoratedUserName(string username)
    {
        return $"user_{username}";
    }
}