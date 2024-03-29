﻿using System.Linq;
using System.Net.Http.Json;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ChatPlatformMobile.Pages;

[QueryProperty("Type", "Type")]
public partial class AuthViewModel : ObservableObject
{
    private readonly SyncService _syncService;
    private readonly IHttpClientFactory _httpClientFactory;

    [ObservableProperty]
    private string _username;
    
    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confirmButtonText;

    [ObservableProperty]
    private string _type;

    public AuthViewModel(SyncService syncService, IHttpClientFactory httpClientFactory)
    {
        _syncService = syncService;
        _httpClientFactory = httpClientFactory;
    }
    
    [RelayCommand]
    private async Task Confirm()
    {
        var endpoint = Type switch
        {
            nameof(AuthenticationType.Login) => "/login",
            nameof(AuthenticationType.Register) => "/register",
            _ => throw new ArgumentOutOfRangeException()
        };

        var client = _httpClientFactory.CreateClient();
        var res = await client.PostAsJsonAsync($"{Constants.Url}/mobile{endpoint}", new DtoAuthUser(Username, Password));

        if(!res.IsSuccessStatusCode)
            return;
        
        var token = await res.Content.ReadAsStringAsync();
        
        Preferences.Default.Set(Constants.TokenKey, token.Trim('"'));

        await _syncService.InitAsync();
        
        await Shell.Current.GoToAsync(nameof(ChatOverviewPage));
    }

    partial void OnTypeChanged(string value)
    {
        ConfirmButtonText = value switch
        {
            nameof(AuthenticationType.Login) => "Login",
            nameof(AuthenticationType.Register) => "Registrieren",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}