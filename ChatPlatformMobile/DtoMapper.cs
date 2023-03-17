using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;

namespace ChatPlatformMobile;

public class DtoMapper
{
    public Chat ToChat(DtoChat dtoChat)
    {
        dtoChat.Messages.Reverse();
        return new Chat
        {
            Messages = new ObservableCollection<DtoMessage>(dtoChat.Messages.Select(x => x with
            {
                Image = $"{Constants.Url}/{x.Image}"
            })),
            Name = dtoChat.Name,
            Usernames = new ObservableCollection<string>(dtoChat.Usernames),
            ChatId = dtoChat.ChatId
        };
    }
}