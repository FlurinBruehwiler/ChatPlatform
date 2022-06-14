using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Hubs;

public class ChatHub : Hub
{
    private readonly ChatAppContext _chatAppContext;
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    public ChatHub(ChatAppContext chatAppContext, IUserService userService, IGroupService groupService)
    {
        _chatAppContext = chatAppContext;
        _userService = userService;
        _groupService = groupService;
    }
    
    public async Task SendMessageToGroupChat(int groupChatId, string messageContent)
    {
        var groupChat = _groupService.GetGroupChatById(groupChatId);
        var user = _userService.GetUserByContextWithGroupChats(Context);

        var message = new Message
        {
            GroupChat = groupChat,
            Content = messageContent,
            User = user,
            DateTime = DateTime.Now,
        };

        var dtoMessage = new DtoMessage(message);

        await Clients.Groups(_groupService.GetUniqueGroupChatName(groupChatId)).SendAsync("ReceiveMessage", dtoMessage);
    }

    public async Task SendMessageToPrivateChat(int privateChatId, string messageContent)
    {
        var user = _userService.GetUserByContext(Context);
        var privateChat = _groupService.GetPrivateChatById(privateChatId);

        var message = new Message
        {
            PrivateChat = privateChat,
            Content = messageContent,
            User = user,
            DateTime = DateTime.Now,
        };

        var dtoMessage = new DtoMessage(message);

        await Clients.Groups(_userService.GetDecoratedUserName(user.Username)).SendAsync("ReceiveMessage", dtoMessage);
    }
}