using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IGroupService
{
    public string GetUniqueGroupChatName(int groupId);
    public PrivateChat GetPrivateChatById(int privateChatId);
    public GroupChat GetGroupChatById(int groupChatId);
}