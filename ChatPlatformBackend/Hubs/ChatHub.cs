using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.Hubs;

[Authorize]
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
        var message = await _messageService.CreateMessageAsync(Context, chatId, messageContent);
        var dtoMessage = new DtoMessage(message.Content, message.User.Username, message.UserId);
        await _chatService.SendMessage(Clients, chatId, dtoMessage);
        _chatAppContext.Messages.Add(message);
        await _chatAppContext.SaveChangesAsync();
    }

    public async Task<int> CreateChat(string name)
    {
        var chat = new Chat
        {
            Name = name,
            Users = new List<User>{await _userService.GetUserByContextAsync(Context)}
        };

        _chatAppContext.Chats.Add(chat);
        await _chatAppContext.SaveChangesAsync();
        return chat.ChatId;
    }

    public async Task AddUserToChat(int chatId, string username)
    {
        var chat = await _chatService.GetChatByIdAsync(chatId);
        var user = await _userService.GetUserByUsernameAsync(username);
        chat.Users.Add(user);
        await _chatAppContext.SaveChangesAsync();
    }

    public async Task RemoveUserFromChat(int chatId, string username)
    {
        var chat = await _chatService.GetChatByIdAsync(chatId);
        var user = await _userService.GetUserByUsernameAsync(username);
        chat.Users.Remove(user);
        await _chatAppContext.SaveChangesAsync();
    }

    public async Task<List<DtoChat>> GetChats()
    {
        var user = _userService.GetUserByContextWithChats(Context);

        var chats = await _chatAppContext.Chats
            .Include(x => x.Users)
            .Where(chat => chat.Users.Any(x => x.Username == user.Username)).ToListAsync();

        return chats.Select(x => new DtoChat(
            x.Users.Select( u => u.Username).ToList(), 
            x.Messages.Select(m => new DtoMessage(m.Content, m.User.Username, m.ChatId)).ToList(), 
            x.Name, 
            x.ChatId)).ToList();
    }
}