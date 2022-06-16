using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IChatService _chatService;
    private readonly IUserService _userService;

    public MessageService(IChatService chatService, IUserService userService)
    {
        _chatService = chatService;
        _userService = userService;
    }
    
    public Message CreateMessage(HubCallerContext context, int chatId, string messageContent)
    {
        var chat = _chatService.GetChatById(chatId);
        var user = _userService.GetUserByContext(context);

        var message = new Message
        {
            Chat = chat,
            Content = messageContent,
            User = user,
            DateTime = DateTime.Now,
        };

        return message;
    }
}