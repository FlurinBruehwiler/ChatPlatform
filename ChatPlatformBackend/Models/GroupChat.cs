namespace ChatPlatformBackend.Models;

public class GroupChat
{
    public int GroupChatId { get; set; }
    
    public string Name { get; set; }

    public List<User> Users { get; set; }
}