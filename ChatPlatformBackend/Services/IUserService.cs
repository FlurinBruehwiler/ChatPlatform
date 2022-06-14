using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services;

public interface IUserService
{
    public User GetUserByContextWithGroupChats(HubCallerContext context);
    public User GetUserByContext(HubCallerContext context);
    public User GetUserById(int id);
    public string GetDecoratedUserName(string username);
}