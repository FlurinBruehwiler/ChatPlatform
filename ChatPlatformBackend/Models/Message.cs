namespace ChatPlatformBackend.Models;

public class Message
{
    public int MessageId { get; set; }
    
    public string Content { get; set; }
    public DateTime DateTime { get; set; }
    
    public int? GroupChatId { get; set; }
    public GroupChat? GroupChat { get; set; }

    public int? PrivateChatId { get; set; }
    public PrivateChat? PrivateChat { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}