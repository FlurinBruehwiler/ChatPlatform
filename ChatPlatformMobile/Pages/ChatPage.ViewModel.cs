using System.Net.Http.Headers;
using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

[QueryProperty("Chat", "Chat")]
public partial class ChatViewModel : ObservableObject, IDisposable
{
    private readonly SyncService _syncService;
    private readonly IHttpClientFactory _httpClientFactory;

    public ChatViewModel(SyncService syncService, IHttpClientFactory httpClientFactory)
    {
        _syncService = syncService;
        _httpClientFactory = httpClientFactory;
    }

    [ObservableProperty] private Chat _chat;

    [ObservableProperty] private string _input;

    private Stream _currentPhoto;
    private string _currentPhotoName;

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

        await UploadPhoto();
        
        await _syncService.SendMessageAsync(Chat.ChatId, Input, _currentPhotoName);
        _currentPhotoName = null;
        Input = string.Empty;
    }

    [RelayCommand]
    private async Task LeaveChat()
    {
        await _syncService.LeaveChatAsync(Chat.ChatId);
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }

    private async Task UploadPhoto()
    {
        if (_currentPhoto is null)
            return;
        
        using var multipartFormContent = new MultipartFormDataContent();
        
        var fileStreamContent = new StreamContent(_currentPhoto);
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        var guid = Guid.NewGuid();

        _currentPhotoName = $"{guid:N}.png";
        
        multipartFormContent.Add(fileStreamContent, name: guid.ToString("N"), fileName: _currentPhotoName);

        var httpClient = _httpClientFactory.CreateClient();

        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Post, $"{Constants.Url}/upload?name={_currentPhotoName}");

        var token = Preferences.Default.Get(Constants.TokenKey, string.Empty);
        
        requestMessage.Headers.Add("Authorization", token);
        requestMessage.Content = multipartFormContent;

        await httpClient.SendAsync(requestMessage);
        
        await _currentPhoto.DisposeAsync();
    }

    [RelayCommand]
    private async Task TakePhoto()
    {
        if (!MediaPicker.Default.IsCaptureSupported) return;
        var photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo == null) return;
        
        _currentPhoto = await photo.OpenReadAsync();
    }

    public void Dispose()
    {
        _currentPhoto?.Dispose();
    }
}