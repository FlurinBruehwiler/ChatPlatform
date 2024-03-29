using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Exceptions;
using ChatPlatformBackend.Factories;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.Services.Implementations;

public class ChatService : IChatService
{
    private readonly ChatAppContext _chatAppContext;
    private readonly IDtoFactory _dtoFactory;

    public ChatService(ChatAppContext chatAppContext, IDtoFactory dtoFactory)
    {
        _chatAppContext = chatAppContext;
        _dtoFactory = dtoFactory;
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

    public async Task<Chat> GetChatByIdWithAsocsAsync(int chatId)
    {
        var chat = await _chatAppContext.Chats.Where(x => x.ChatId == chatId)
            .Include(x => x.Users)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync();
        if (chat is null)
            throw new BadRequestException(Errors.ChatNotFound);
        return chat;
    }

    public Task SendMessage(IHubCallerClients clients, int chatId, DtoMessage message)
    {
        return clients.Groups(GetUniqueChatName(chatId)).SendAsync("ReceiveMessage", message);
    }

    public async Task InviteUserToChatAsync(IHubCallerClients clients, User user, Chat chat)
    {
        await clients.Groups(user.Username).SendAsync("ReceiveInvite", chat.ChatId);
    }
}