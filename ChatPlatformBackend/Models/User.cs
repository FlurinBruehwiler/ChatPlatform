namespace ChatPlatformBackend.Models;

public class User
{
    public int UserId { get; set; }

    public string? PicturePath { get; set; }
    public string Username { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;

    public List<Message> Messages { get; set; } = null!;
    public List<Chat> Chats { get; set; } = null!;
}