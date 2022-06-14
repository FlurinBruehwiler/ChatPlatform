namespace ChatPlatformBackend.Models;

public class Chat
{
    public int ChatId { get; set; }

    public List<GroupChat> GroupChats { get; set; }
    public List<PrivateChat> PrivateChats { get; set; }
    public List<Message> Messages { get; set; }
}