using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    private async Task InitAsync()
    {
        var token =  Preferences.Default.Get(Constants.TokenKey, string.Empty);
        
        if(token == string.Empty)
            await Shell.Current.GoToAsync(nameof(WelcomePage));
        else       
            await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }
}