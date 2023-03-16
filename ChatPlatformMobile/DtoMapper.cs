using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;

namespace ChatPlatformMobile;

public class DtoMapper
{
    public Chat ToChat(DtoChat dtoChat)
    {
        return new Chat
        {
            Messages = new ObservableCollection<DtoMessage>(dtoChat.Messages),
            Name = dtoChat.Name,
            Usernames = new ObservableCollection<string>(dtoChat.Usernames),
            ChatId = dtoChat.ChatId
        };
    }
}