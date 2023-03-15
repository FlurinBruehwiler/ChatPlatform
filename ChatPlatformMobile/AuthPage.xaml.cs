using System.Net.Http.Json;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;

namespace ChatPlatformMobile;

public partial class AuthPage
{
    private readonly AuthenticationType _authenticationType;
    private const string TokenKey = "TokenKey";
    
    public AuthPage(AuthenticationType authenticationType)
    {
        InitializeComponent();

        _authenticationType = authenticationType;
        ConfirmButton.Text = _authenticationType switch
        {
            AuthenticationType.Login => "Login",
            AuthenticationType.Register => "Registrieren",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        var endpoint = _authenticationType switch
        {
            AuthenticationType.Login => "/login",
            AuthenticationType.Register => "/register",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var client = new HttpClient();
        var res = await client.PostAsJsonAsync($"https://localhost:7087/mobile{endpoint}", new DtoAuthUser(Username.Text, Password.Text));

        if(!res.IsSuccessStatusCode)
            return;
        
        var token = await res.Content.ReadAsStringAsync();
        
        Preferences.Default.Set(TokenKey, token);
        
        await Navigation.PushAsync(new ChatOverviewPage());
    }
}