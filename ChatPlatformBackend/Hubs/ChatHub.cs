using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformBackend.Hubs;

public class ChatHub : Hub
{
    private readonly ChatAppContext _chatAppContext;
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;

    public ChatHub(ChatAppContext chatAppContext, IChatService chatService, IMessageService messageService, IUserService userService)
    {
        _chatAppContext = chatAppContext;
        _chatService = chatService;
        _messageService = messageService;
        _userService = userService;
    }
    
    public async Task SendMessage(int chatId, string messageContent)
    {
        var message = _messageService.CreateMessage(Context, chatId, messageContent);
        var dtoMessage = new DtoMessage(message);
        await _chatService.SendMessage(Clients, chatId, dtoMessage);
        _chatAppContext.Messages.Add(message);
        await _chatAppContext.SaveChangesAsync();
    }

    public async Task<int> CreateChat(string name)
    {
        var chat = new Chat
        {
            Name = name,
            Users = new List<User>{_userService.GetUserByContext(Context)}
        };

        _chatAppContext.Chats.Add(chat);
        await _chatAppContext.SaveChangesAsync();
        return chat.ChatId;
    }

    public async Task AddUserToChat(int chatId, int userId)
    {
        var chat = _chatService.GetChatById(chatId);
        var user = _userService.GetUserById(userId);
        chat.Users.Add(user);
        await _chatAppContext.SaveChangesAsync();
    }

    public async Task RemoveUserFromChat(int chatId, int userId)
    {
        var chat = _chatService.GetChatById(chatId);
        var user = _userService.GetUserById(userId);
        chat.Users.Remove(user);
        await _chatAppContext.SaveChangesAsync();
    }
}