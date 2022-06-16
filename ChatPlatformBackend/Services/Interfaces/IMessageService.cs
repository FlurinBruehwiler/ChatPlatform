using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IMessageService
{
    public Message CreateMessage(HubCallerContext context, int chatId, string messageContent);
}