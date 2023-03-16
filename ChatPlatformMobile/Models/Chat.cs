using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatPlatformMobile.Models;

public partial class Chat : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<string> _usernames;

    [ObservableProperty] 
    private ObservableCollection<DtoMessage> _messages;

    [ObservableProperty] 
    private string _name;

    [ObservableProperty] 
    private int _chatId;
}