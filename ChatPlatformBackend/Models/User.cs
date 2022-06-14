namespace ChatPlatformBackend.Models;

public class User
{
    public int UserId { get; set; }

    public string picturePath { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public List<Message> Messages { get; set; }
    public List<Chat> Chats { get; set; }
}