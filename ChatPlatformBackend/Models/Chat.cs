namespace ChatPlatformBackend.Models;

public class Chat
{
    public int ChatId { get; set; }
    
    public string Name { get; set; }

    public List<User> Users { get; set; }
}