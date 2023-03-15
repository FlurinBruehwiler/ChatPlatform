
using ChatPlatformBackend.DtoModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatPlatformMobile;

public class SyncService
{
    private readonly HubConnection _hubConnection;
    
    public SyncService()
    {
        _hubConnection  = new HubConnectionBuilder()
            .WithUrl("http://localhost:53353/ChatHub")
            .Build();
    }

    public async Task Start()
    {
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

    public async Task SendMessage(int chatId, string messageContent)
    {
        await _hubConnection.InvokeAsync("SendMessage", chatId, messageContent);
    }
    
    public async Task CreateChat(string name, List<string> users)
    {
        await _hubConnection.InvokeAsync("CreateChat", name, users);
    }
    
    public async Task AddUserToChat(int chatId, string username)
    {
        await _hubConnection.InvokeAsync("AddUserToChat", chatId, username);
    }
    
    public async Task RemoveUserFromChat(int chatId, string username)
    {
        await _hubConnection.InvokeAsync("RemoveUserFromChat", chatId, username);
    }
    
    public async Task<List<DtoChat>> GetChats()
    {
        return await _hubConnection.InvokeAsync<List<DtoChat>>("GetChats");
    }
    
    public async Task<DtoChat> JoinChat(int chatId)
    {
        return await _hubConnection.InvokeAsync<DtoChat>("JoinChat", chatId);
    }
    
    public async Task LeaveChat(int chatId)
    {
        await _hubConnection.InvokeAsync("LeaveChat", chatId);
    }
    
    public async Task KickChat(int chatId)
    {
        await _hubConnection.InvokeAsync("KickChat", chatId);
    }    
    
    public async Task<DtoUser> GetCurrentUser()
    {
        return await _hubConnection.InvokeAsync<DtoUser>("GetCurrentUser");
    }
}