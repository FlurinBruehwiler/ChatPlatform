using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;
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
    private ObservableCollection<CheckedUser> _users;

    [RelayCommand]
    private async Task CreateChat()
    {
        if (string.IsNullOrEmpty(ChatName))
            return;

        var usernames = Users
            .Where(x => x.Enabled)
            .Select(x => x.User.Username)
            .ToList();
        
        await _syncService.CreateChatAsync(ChatName, usernames);
        
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }

    [RelayCommand]
    private async Task InitAsync()
    {
        var users = await _syncService.GetAvailableUsersAsync();
        Users = new ObservableCollection<CheckedUser>(users.Select(x => new CheckedUser(x, false)));
    }
}