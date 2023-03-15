using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class ChatOverviewViewModel : ObservableObject
{
    private readonly SyncService _syncService;

    public ChatOverviewViewModel(SyncService syncService)
    {
        _syncService = syncService;
        Chats = new ObservableCollection<DtoChat>();
    }
    
    [ObservableProperty]
    private ObservableCollection<DtoChat> _chats;

    [RelayCommand]
    private async Task InitAsync()
    {
        await _syncService.StartAsync();
    }
    
    [RelayCommand]
    private void CreateChat()
    {
        
    }
}