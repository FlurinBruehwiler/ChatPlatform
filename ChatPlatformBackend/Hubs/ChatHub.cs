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
    private readonly IChatService _chatService;

    public ChatHub(ChatAppContext chatAppContext, IUserService userService, IChatService chatService)
    {
        _chatAppContext = chatAppContext;
        _userService = userService;
        _chatService = chatService;
    }
    
    public async Task SendMessage(int chatId, string messageContent)
    {
        var chat = _chatService.GetChatById(chatId);
        var user = _userService.GetUserByContext(Context);

        var message = new Message
        {
            Chat = chat,
            Content = messageContent,
            User = user,
            DateTime = DateTime.Now,
        };

        var dtoMessage = new DtoMessage(message);

        await Clients.Groups(_chatService.GetUniqueChatName(chatId)).SendAsync("ReceiveMessage", dtoMessage);

        _chatAppContext.Messages.Add(message);
        await _chatAppContext.SaveChangesAsync();
    }
}