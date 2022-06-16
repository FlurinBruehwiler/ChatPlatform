using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Implementations;

public class ChatService : IChatService
{
    private readonly IChatService _chatService;

    public ChatService(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    public string GetUniqueChatName(int groupId)
    {
        throw new NotImplementedException();
    }

    public Chat GetChatById(int chatId)
    {
        throw new NotImplementedException();
    }

    public Task SendMessage(IHubCallerClients clients, int chatId, DtoMessage message)
    {
        return clients.Groups(_chatService.GetUniqueChatName(chatId)).SendAsync("ReceiveMessage", message);
    }
}