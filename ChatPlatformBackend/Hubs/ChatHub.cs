using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Hubs;

public class ChatHub : Hub
{
    private readonly ChatAppContext _chatAppContext;
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;

    public ChatHub(ChatAppContext chatAppContext, IChatService chatService, IMessageService messageService)
    {
        _chatAppContext = chatAppContext;
        _chatService = chatService;
        _messageService = messageService;
    }
    
    public async Task SendMessage(int chatId, string messageContent)
    {
        var message = _messageService.CreateMessage(Context, chatId, messageContent);
        var dtoMessage = new DtoMessage(message);
        await _chatService.SendMessage(Clients, chatId, dtoMessage);
        _chatAppContext.Messages.Add(message);
        await _chatAppContext.SaveChangesAsync();
    }
}