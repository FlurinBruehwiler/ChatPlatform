using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.DtoModels;

public class DtoMessage
{
    public DtoMessage(Message message)
    {
        MessageContent = message.Content;
        Username = message.User.Username;
    }

    public string MessageContent { get; set; }
    public string Username { get; set; }
}