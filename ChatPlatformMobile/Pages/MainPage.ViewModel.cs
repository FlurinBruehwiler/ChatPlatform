using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class MainViewModel : ObservableObject
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MainViewModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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

        var res = await client.GetAsync($"{Constants.Url}/protected");

        if (!res.IsSuccessStatusCode)
        {
            await Shell.Current.GoToAsync(nameof(WelcomePage));
            return;
        }
        
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }
}