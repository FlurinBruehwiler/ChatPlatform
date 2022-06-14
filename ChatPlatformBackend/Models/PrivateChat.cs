namespace ChatPlatformBackend.Models;

public class PrivateChat
{
    public int PrivateChatId { get; set; }

    public List<User> Users { get; set; }
}