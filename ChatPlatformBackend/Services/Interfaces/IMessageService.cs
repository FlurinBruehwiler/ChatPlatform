using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IMessageService
{
    public Task<Message> CreateMessageAsync(HubCallerContext context, int chatId, string messageContent, string? image);
}