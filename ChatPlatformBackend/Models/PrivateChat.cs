namespace ChatPlatformBackend.Models;

public class PrivateChat
{
    public int PrivateChatId { get; set; }

    public int ChatId { get; set; }

    public int User1Id { get; set; }
    public User User1 { get; set; }

    public int User2Id { get; set; }
    public User User2 { get; set; }
}