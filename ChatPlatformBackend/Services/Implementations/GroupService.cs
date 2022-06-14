using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;

namespace ChatPlatformBackend.Services.Implementations;

public class GroupService : IGroupService
{
    public string GetUniqueGroupChatName(int groupId)
    {
        return $"groupChat_{groupId}";
    }

    public PrivateChat GetPrivateChatById(int privateChatId)
    {
        throw new NotImplementedException();
    }

    public GroupChat GetGroupChatById(int groupChatId)
    {
        throw new NotImplementedException();
    }
}