
using ChatPlatformBackend.DtoModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatPlatformMobile;

public class SyncService
{
    private HubConnection _hubConnection;
    
    public async Task StartAsync()
    {
        var token =  Preferences.Default.Get(Constants.TokenKey, string.Empty);
        
        _hubConnection  = new HubConnectionBuilder()
            .WithUrl($"{Constants.Url}/ChatHub", options =>
            {
                options.Headers.Add("Authorization", token);
            })
            .Build();
        
        _hubConnection.On<DtoMessage>("ReceiveMessage", ReceiveMessage);
        _hubConnection.On<int>("ReceiveInvite", ReceiveInvite);
        _hubConnection.On<int>("ReceiveKick", ReceiveKick);
        
        await _hubConnection.StartAsync();
    }

    private void ReceiveKick(int chatId)
    {
        
    }

    private void ReceiveInvite(int chatId)
    {
        
    }

    private void ReceiveMessage(DtoMessage dtoMessage)
    {
        
    }

    public async Task SendMessageAsync(int chatId, string messageContent)
    {
        await _hubConnection.InvokeAsync("SendMessage", chatId, messageContent);
    }
    
    public async Task CreateChatAsync(string name, List<string> users)
    {
        await _hubConnection.InvokeAsync("CreateChat", name, users);
    }
    
    public async Task AddUserToChatAsync(int chatId, string username)
    {
        await _hubConnection.InvokeAsync("AddUserToChat", chatId, username);
    }
    
    public async Task RemoveUserFromChatAsync(int chatId, string username)
    {
        await _hubConnection.InvokeAsync("RemoveUserFromChat", chatId, username);
    }
    
    public async Task<List<DtoChat>> GetChatsAsync()
    {
        return await _hubConnection.InvokeAsync<List<DtoChat>>("GetChats");
    }
    
    public async Task<DtoChat> JoinChatAsync(int chatId)
    {
        return await _hubConnection.InvokeAsync<DtoChat>("JoinChat", chatId);
    }
    
    public async Task LeaveChatAsync(int chatId)
    {
        await _hubConnection.InvokeAsync("LeaveChat", chatId);
    }
    
    public async Task KickChatAsync(int chatId)
    {
        await _hubConnection.InvokeAsync("KickChat", chatId);
    }    
    
    public async Task<DtoUser> GetCurrentUserAsync()
    {
        return await _hubConnection.InvokeAsync<DtoUser>("GetCurrentUser");
    }
}