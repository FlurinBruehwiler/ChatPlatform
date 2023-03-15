using ChatPlatformMobile.Models;

namespace ChatPlatformMobile;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Register(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AuthPage(AuthenticationType.Register));
    }

    private async void Login(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AuthPage(AuthenticationType.Login));
    }
}