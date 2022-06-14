using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Services;

public interface IGroupService
{
    public string GetUniqueGroupName(int groupId);
    public PrivateChat GetPrivateChatById(int privateChatId);
    public GroupChat GetGroupChatById(int groupChatId);
}