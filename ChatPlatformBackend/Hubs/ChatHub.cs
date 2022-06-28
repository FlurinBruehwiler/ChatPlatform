using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Factories;
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
    private readonly IDtoFactory _dtoFactory;

    public ChatHub(ChatAppContext chatAppContext, IChatService chatService, IMessageService messageService, IUserService userService, IDtoFactory dtoFactory)
    {
        _chatAppContext = chatAppContext;
        _chatService = chatService;
        _messageService = messageService;
        _userService = userService;
        _dtoFactory = dtoFactory;
    }
    
    public async Task SendMessage(int chatId, string messageContent)
    {
        var message = await _messageService.CreateMessageAsync(Context, chatId, messageContent);
        _chatAppContext.Messages.Add(message);
        await _chatAppContext.SaveChangesAsync();
        var dtoMessage = new DtoMessage(message.Content, message.User.Username, chatId, message.MessageId);
        await _chatService.SendMessage(Clients, chatId, dtoMessage);
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
        await _chatService.AddUserToGroup(Groups, Context, chat);
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
            .Include(x => x.Messages)
            .Where(chat => chat.Users.Any(x => x.Username == user.Username)).ToListAsync();

        var dtoChats = chats.Select(x => _dtoFactory.CreateDtoChat(x)).ToList();

        return dtoChats;
    }
}