using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IChatService
{
    public string GetUniqueChatName(int groupId);
    public Chat GetChatById(int chatId);
}