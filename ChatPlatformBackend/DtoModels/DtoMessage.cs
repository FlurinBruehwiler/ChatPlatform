using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.DtoModels;

public class DtoMessage
{
    public DtoMessage(Message message)
    {
        MessageContent = message.Content;
        Username = message.User.Username;
        ChatId = message.GroupChatId ?? message.PrivateChatId ?? throw new Exception();
    }

    public string MessageContent { get; set; }
    public string Username { get; set; }
    public int ChatId { get; set; }
}