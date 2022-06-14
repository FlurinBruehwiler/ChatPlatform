using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services;

public class UserService : IUserService
{
    public User GetUserWithGroupChats(HubCallerContext context)
    {
        throw new NotImplementedException();
    }

    public string GetDecoratedUserName(string username)
    {
        return $"user_{username}";
    }
}