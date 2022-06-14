namespace ChatPlatformBackend.Services;

public class GroupService : IGroupService
{
    public string GetUniqueGroupName(int groupId)
    {
        return $"groupChat_{groupId}";
    }
}