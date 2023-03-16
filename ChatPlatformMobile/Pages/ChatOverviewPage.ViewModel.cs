using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class ChatOverviewViewModel : ObservableObject
{
    
    [ObservableProperty]
    private SyncService _syncService;

    public ChatOverviewViewModel(SyncService syncService)
    {
        _syncService = syncService;
    }

    [RelayCommand]
    private async Task ChatClick(Chat chat)
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
        MauiProgram.DeleteSyncService();
        Preferences.Default.Set(Constants.TokenKey, string.Empty);
        await Shell.Current.GoToAsync(nameof(WelcomePage));
    }
}