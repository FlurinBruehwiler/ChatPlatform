using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class CreateChatViewModel : ObservableObject
{
    private readonly SyncService _syncService;

    public CreateChatViewModel(SyncService syncService)
    {
        _syncService = syncService;
    }
    
    [ObservableProperty]
    private string _chatName;

    [ObservableProperty]
    private ObservableCollection<DtoUser> _users;

    [RelayCommand]
    private async Task CreateChat()
    {
        if (string.IsNullOrEmpty(ChatName))
            return;
        
        await _syncService.CreateChatAsync(ChatName, new List<string>());
        
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }
}