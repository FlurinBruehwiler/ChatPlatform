using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.Services.Implementations;

public class UserService : IUserService
{
    private readonly ChatAppContext _chatAppContext;

    public UserService(ChatAppContext chatAppContext)
    {
        _chatAppContext = chatAppContext;
    }
    
    public User GetUserByContextWithChats(HubCallerContext context)
    {
        if(context.User?.Identity is null)
            throw new Exception("No authentication provided");
        
        var user = _chatAppContext.Users.Where(x => x.Username == context.User.Identity.Name)
            .Include(x => x.Chats).FirstOrDefault(); 
        
        if(user is null)
            throw new Exception($"User with username {context.User.Identity.Name} does not exist");

        return user;
    }

    public async Task<User> GetUserByContextAsync(HubCallerContext context)
    {
        if(context.User?.Identity is null)
            throw new Exception("No authentication provided");
        
        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == context.User.Identity.Name); 
        
        if(user is null)
            throw new Exception($"User with username {context.User.Identity.Name} does not exist");

        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
        if(user is null)
            throw new Exception($"User with id {id} does not exist");
        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _chatAppContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        if(user is null)
            throw new Exception($"User with id {user} does not exist");
        return user;
    }
}