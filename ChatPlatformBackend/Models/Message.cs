namespace ChatPlatformBackend.Models;

public class Message
{
    public int MessageId { get; set; }
    
    public string Content { get; set; } = null!;
    public DateTime DateTime { get; set; }
    
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string? Image { get; set; }
}