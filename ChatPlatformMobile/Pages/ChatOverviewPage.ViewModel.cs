using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class ChatOverviewViewModel : ObservableObject
{
    private readonly SyncService _syncService;

    [ObservableProperty]
    private ObservableCollection<DtoChat> _chats;

    public ChatOverviewViewModel(SyncService syncService)
    {
        _syncService = syncService;
    }

    [RelayCommand]
    private async Task InitAsync()
    {
        await _syncService.StartAsync();
        Chats = new ObservableCollection<DtoChat>(_syncService.Chats);
    }
    
    [RelayCommand]
    private async Task ChatClick(DtoChat chat)
    {
        await Shell.Current.GoToAsync(nameof(ChatPage), new Dictionary<string, object>
        {
            {"Chat", chat}
        });
    }
    
    [RelayCommand]
    private async Task CreateChat()
    {
        await Shell.Current.GoToAsync(nameof(CreateChatPage));
    }
    
    [RelayCommand]
    private async Task Logout()
    {
        Preferences.Default.Set(Constants.TokenKey, string.Empty);
        await Shell.Current.GoToAsync(nameof(WelcomePage));
    }
}