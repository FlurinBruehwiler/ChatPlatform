using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Filters;

public class GroupFilter : IHubFilter
{
    private readonly IUserService _userService;
    private readonly IChatService _chatService;

    public GroupFilter(IUserService userService, IChatService chatService)
    {
        _userService = userService;
        _chatService = chatService;
    }
    
    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        return await next(invocationContext);
    }
    
    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var user = _userService.GetUserByContextWithChats(context.Context);
        foreach (var chat in user.Chats)
        {
            context.Hub.Groups.AddToGroupAsync(context.Context.ConnectionId, _chatService.GetUniqueChatName(chat.ChatId));
        }

        context.Hub.Groups.AddToGroupAsync(context.Context.ConnectionId, user.Username);
        
        return next(context);
    }

    public Task OnDisconnectedAsync(
        HubLifetimeContext context, Exception? exception, Func<HubLifetimeContext, Exception, Task> next)
    {
        return next(context, exception!);
    }
}