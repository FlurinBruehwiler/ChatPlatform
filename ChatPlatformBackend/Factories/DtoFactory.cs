using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Factories;

public class DtoFactory : IDtoFactory
{
    public DtoChat CreateDtoChat(Chat chat)
    {
        return new DtoChat(
            chat.Users.Select(u => u.Username).ToList(),
            chat.Messages.Select(CreateDtoMessage).ToList(),
            chat.Name,
            chat.ChatId);
    }

    public DtoMessage CreateDtoMessage(Message message)
    {
        return new DtoMessage(message.Content, message.User.Username, message.ChatId, message.MessageId);
    }

    public DtoUser CreateDtoUser(User user)
    {
        return new DtoUser(user.Username, user.PicturePath);
    }
}