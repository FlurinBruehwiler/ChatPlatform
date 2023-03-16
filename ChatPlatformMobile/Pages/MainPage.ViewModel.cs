using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class MainViewModel : ObservableObject
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SyncService _syncService;

    public MainViewModel(IHttpClientFactory httpClientFactory, SyncService syncService)
    {
        _httpClientFactory = httpClientFactory;
        _syncService = syncService;
    }

    [RelayCommand]
    private async Task InitAsync()
    {
        var token = Preferences.Default.Get(Constants.TokenKey, string.Empty);

        if (token == string.Empty)
        {
            await Shell.Current.GoToAsync(nameof(WelcomePage));
            return;
        }

        var client = _httpClientFactory.CreateClient();

        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, $"{Constants.Url}/protected");

        requestMessage.Headers.Add("Authorization", token);

        var res = await client.SendAsync(requestMessage);

        if (!res.IsSuccessStatusCode)
        {
            await Shell.Current.GoToAsync(nameof(WelcomePage));
            return;
        }

        await _syncService.InitAsync();

        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }
}