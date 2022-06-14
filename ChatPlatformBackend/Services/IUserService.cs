using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services;

public interface IUserService
{
    public User GetUserWithGroupChats(HubCallerContext context);
    public string GetDecoratedUserName(string username);
}