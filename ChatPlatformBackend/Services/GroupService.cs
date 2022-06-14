using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Services;

public class GroupService : IGroupService
{
    public string GetUniqueGroupName(int groupId)
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