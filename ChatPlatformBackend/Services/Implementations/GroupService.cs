using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;

namespace ChatPlatformBackend.Services.Implementations;

public class GroupService : IGroupService
{
    public string GetUniqueChatName(int chatId)
    {
        return $"chat_{chatId}";
    }

    public Chat GetChatById(int chatId)
    {
        throw new NotImplementedException();
    }
}