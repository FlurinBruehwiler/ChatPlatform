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
    
    public async Task<Message> CreateMessageAsync(HubCallerContext context, int chatId, string messageContent,
        string? image)
    {
        var chat = await _chatService.GetChatByIdAsync(chatId);
        var user = await _userService.GetUserByContextAsync(context);

        var message = new Message
        {
            Chat = chat,
            Content = messageContent,
            User = user,
            DateTime = DateTime.Now,
            Image = image
        };

        return message;
    }
}