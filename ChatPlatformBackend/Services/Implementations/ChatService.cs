using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Exceptions;
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
            throw new BadRequestException(Errors.ChatNotFound);
        return chat;
    }

    public Task SendMessage(IHubCallerClients clients, int chatId, DtoMessage message)
    {
        return clients.Groups(GetUniqueChatName(chatId)).SendAsync("ReceiveMessage", message);
    }

    public Task AddUserToGroup(HubLifetimeContext context, Chat chat)
    {
        return context.Hub.Groups.AddToGroupAsync(context.Context.ConnectionId, GetUniqueChatName(chat.ChatId));
    }
}