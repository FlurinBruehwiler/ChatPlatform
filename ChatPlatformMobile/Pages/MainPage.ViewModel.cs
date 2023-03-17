using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class MainViewModel : ObservableObject
{
    private readonly SyncService _syncService;

    public MainViewModel(SyncService syncService)
    {
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

        var handlerService = new HttpsClientHandlerService();
        var client = new HttpClient(handlerService.GetPlatformMessageHandler());

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