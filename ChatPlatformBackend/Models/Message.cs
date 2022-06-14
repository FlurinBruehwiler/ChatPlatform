namespace ChatPlatformBackend.Models;

public class Message
{
    public int MessageId { get; set; }
    
    public string Content { get; set; }
    public DateTime DateTime { get; set; }
    
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}