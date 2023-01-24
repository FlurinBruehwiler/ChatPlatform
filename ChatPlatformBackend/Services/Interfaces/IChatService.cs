using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IChatService
{
    public string GetUniqueChatName(int groupId);
    public Task<Chat> GetChatByIdAsync(int chatId);
    public Task<Chat> GetChatByIdWithAsocsAsync(int chatId);
    public Task SendMessage(IHubCallerClients clients ,int chatId, DtoMessage message);
    public Task InviteUserToChatAsync(IHubCallerClients clients, User user, Chat chat);
}