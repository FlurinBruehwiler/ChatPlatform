using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

[QueryProperty("Chat", "Chat")]
public partial class ChatViewModel : ObservableObject
{
    private readonly SyncService _syncService;

    public ChatViewModel(SyncService syncService)
    {
        _syncService = syncService;
    }

    [ObservableProperty] private Chat _chat;

    [ObservableProperty] private string _input;

    [RelayCommand]
    private void Init()
    {
        _syncService.RegisterLeaveChatEvent(Chat,
            () =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
                });
            });
    }

    [RelayCommand]
    private async Task Completed()
    {
        if (string.IsNullOrEmpty(Input))
            return;

        await _syncService.SendMessageAsync(Chat.ChatId, Input);
        Input = string.Empty;
    }

    [RelayCommand]
    private async Task LeaveChat()
    {
        await _syncService.LeaveChatAsync(Chat.ChatId);
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }
}