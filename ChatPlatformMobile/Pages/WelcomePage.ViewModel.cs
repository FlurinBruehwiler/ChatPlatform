using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatPlatformMobile.Pages;

public partial class WelcomeViewModel : ObservableObject
{
    [RelayCommand]
    private async Task Register()
    {
        await Shell.Current.GoToAsync($"{nameof(AuthPage)}?Type={AuthenticationType.Register}");
    }
    
    [RelayCommand]
    private async Task Login()
    {
        await Shell.Current.GoToAsync($"{nameof(AuthPage)}?Type={AuthenticationType.Login}");
    }
}