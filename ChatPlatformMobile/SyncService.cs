
using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;
using ChatPlatformMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatPlatformMobile;

public partial class SyncService : ObservableObject
{
    private readonly DtoMapper _dtoMapper;
    private HubConnection _hubConnection;

    [ObservableProperty] 
    private ObservableCollection<Chat> _chats;

    [ObservableProperty]
    private DtoUser _currentUser;
    
    
    public SyncService(DtoMapper dtoMapper)
    {
        _dtoMapper = dtoMapper;
    }

    public async Task InitAsync()
    {
        var token =  Preferences.Default.Get(Constants.TokenKey, string.Empty);
        
        _hubConnection  = new HubConnectionBuilder()
            .WithUrl($"{Constants.Url}/ChatHub", options =>
            {
                options.Headers.Add("Authorization", token);
                options.HttpMessageHandlerFactory = _ =>
                {
                    var handlerService = new HttpsClientHandlerService();
                    return handlerService.GetPlatformMessageHandler();
                };
            })
            .Build();
        
        _hubConnection.On<DtoMessage>("ReceiveMessage", ReceiveMessage);
        _hubConnection.On<int>("ReceiveInvite", ReceiveInvite);
        _hubConnection.On<int>("ReceiveKick", ReceiveKick);
        
        await _hubConnection.StartAsync();

        var chats = await GetChatsAsync();
        CurrentUser = await GetCurrentUserAsync();

        Chats = new ObservableCollection<Chat>(chats.Select(x => _dtoMapper.ToChat(x)));
    }
    
    private async Task ReceiveKick(int chatId)
    {
        await KickChatAsync(chatId);
        var chat = Chats.First(x => x.ChatId == chatId);

        if (_leaveChatEvent.HasValue)
        {
            if (_leaveChatEvent.Value.chat == chat)
                _leaveChatEvent.Value.callback();
        }
        
        Chats.Remove(chat);
    }

    private (Chat chat, Action callback)? _leaveChatEvent;

    public void RegisterLeaveChatEvent(Chat chat, Action callback)
    {
        _leaveChatEvent = (chat, callback);
    }

    private async Task ReceiveInvite(int chatId)
    {
        var chat = await JoinChatAsync(chatId);
        Chats.Add(_dtoMapper.ToChat(chat));
    }

    private void ReceiveMessage(DtoMessage dtoMessage)
    {
        Chats.First(x => x.ChatId == dtoMessage.ChatId).Messages.Add(dtoMessage);
    }

    public async Task SendMessageAsync(int chatId, string messageContent, string? image)
    {
        await _hubConnection.InvokeAsync("SendMessage", chatId, messageContent, image);
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
    
    private async Task<List<DtoChat>> GetChatsAsync()
    {
        return await _hubConnection.InvokeAsync<List<DtoChat>>("GetChats");
    }

    private async Task<DtoChat> JoinChatAsync(int chatId)
    {
        return await _hubConnection.InvokeAsync<DtoChat>("JoinChat", chatId);
    }
    
    public async Task LeaveChatAsync(int chatId)
    {
        await _hubConnection.InvokeAsync("LeaveChat", chatId);
    }

    private async Task KickChatAsync(int chatId)
    {
        await _hubConnection.InvokeAsync("KickChat", chatId);
    }

    private async Task<DtoUser> GetCurrentUserAsync()
    {
        return await _hubConnection.InvokeAsync<DtoUser>("GetCurrentUser");
    }
    
    public async Task<List<DtoUser>> GetAvailableUsersAsync()
    {
        return await _hubConnection.InvokeAsync<List<DtoUser>>("GetAvailableUsers");
    }
}