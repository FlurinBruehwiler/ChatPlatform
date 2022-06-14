using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services;

public class UserService : IUserService
{
    public User GetUserByContextWithGroupChats(HubCallerContext context)
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