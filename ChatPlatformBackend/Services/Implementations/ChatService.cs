using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.Services.Implementations;

public class ChatService : IChatService
{
    private readonly ChatAppContext _chatAppContext;

    public ChatService(ChatAppContext chatAppContext)
    {
        _chatAppContext = chatAppContext;
    }
    
    public string GetUniqueChatName(int groupId)
    {
        return $"chat_{groupId}";
    }

    public async Task<Chat> GetChatByIdAsync(int chatId)
    {
        var chat = await _chatAppContext.Chats.FirstOrDefaultAsync(x => x.ChatId == chatId);
        if (chat is null)
            throw new Exception($"Chat with id {chatId} does not exist");
        return chat;
    }

    public Task SendMessage(IHubCallerClients clients, int chatId, DtoMessage message)
    {
        return clients.Groups(GetUniqueChatName(chatId)).SendAsync("ReceiveMessage", message);
    }
}