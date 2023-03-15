using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Factories;

public interface IDtoFactory
{
    public DtoChat CreateDtoChat(Chat chat);
    public DtoMessage CreateDtoMessage(Message message);
    public DtoUser CreateDtoUser(User user);
}