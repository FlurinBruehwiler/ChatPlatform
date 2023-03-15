using System.Net.Http.Json;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ChatPlatformMobile.Pages;

[QueryProperty("_authenticationType", "Type")]
public partial class AuthViewModel : ObservableObject
{
    [ObservableProperty]
    private string _username;
    
    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confirmButtonText;

    [ObservableProperty]
    private AuthenticationType _authenticationType;
    
    [RelayCommand]
    private async Task Confirm()
    {
        var endpoint = AuthenticationType switch
        {
            AuthenticationType.Login => "/login",
            AuthenticationType.Register => "/register",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var client = new HttpClient();
        var res = await client.PostAsJsonAsync($"{Constants.Url}/mobile{endpoint}", new DtoAuthUser(Username, Password));

        if(!res.IsSuccessStatusCode)
            return;
        
        var token = await res.Content.ReadAsStringAsync();
        
        Preferences.Default.Set(Constants.TokenKey, token.Trim('"'));

        var syncService = new SyncService();

        await syncService.StartAsync();
        
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }

    partial void OnAuthenticationTypeChanged(AuthenticationType value)
    {
        ConfirmButtonText = value switch
        {
            AuthenticationType.Login => "Login",
            AuthenticationType.Register => "Registrieren",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}