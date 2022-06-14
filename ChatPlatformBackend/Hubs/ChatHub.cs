using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services;
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
        var groupChat = _chatAppContext.GroupChats.FirstOrDefault(x => x.GroupChatId == groupChatId);
        var user = _userService.GetUserWithGroupChats(Context);

        var message = new Message
        {
            GroupChat = groupChat,
            Content = messageContent,
            User = user,
            DateTime = DateTime.Now,
        };

        var dtoMessage = new DtoMessage(message);

        await Clients.Groups(_groupService.GetUniqueGroupName(groupChatId)).SendAsync("ReceiveMessage", dtoMessage);
    }

    public Task SendMessageToPrivateChat()
    {
        throw new NotImplementedException();
    }
}