using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend;

public class GroupFilter : IHubFilter
{
    private readonly ChatAppContext _chatAppContext;
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    public GroupFilter(ChatAppContext chatAppContext, IUserService userService, IGroupService groupService)
    {
        _chatAppContext = chatAppContext;
        _userService = userService;
        _groupService = groupService;
    }
    
    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        return await next(invocationContext);
    }
    
    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var user = _userService.GetUserWithGroupChats(context.Context);
        foreach (var groupChat in user.GroupChats)
        {
            context.Hub.Groups.AddToGroupAsync(context.Context.ConnectionId, _groupService.GetUniqueGroupName(groupChat.GroupChatId));
        }

        context.Hub.Groups.AddToGroupAsync(context.Context.ConnectionId,  _userService.GetDecoratedUserName(user.Username));

        return next(context);
    }

    public Task OnDisconnectedAsync(
        HubLifetimeContext context, Exception? exception, Func<HubLifetimeContext, Exception, Task> next)
    {
        return next(context, exception!);
    }
}