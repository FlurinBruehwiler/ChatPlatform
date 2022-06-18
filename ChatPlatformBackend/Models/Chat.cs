namespace ChatPlatformBackend.Models;

public class Chat
{
    public int ChatId { get; set; }
    
    public string Name { get; set; } = null!;

    public List<User> Users { get; set; } = null!;
}