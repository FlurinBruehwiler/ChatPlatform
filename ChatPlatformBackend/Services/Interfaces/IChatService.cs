using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IChatService
{
    public string GetUniqueChatName(int groupId);
    public Chat GetChatById(int chatId);
    public Task SendMessage(IHubCallerClients clients ,int chatId, DtoMessage message);
}