using ChatPlatformBackend.DtoModels;
using ChatPlatformBackend.Exceptions;
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

    public async Task CreateChat(string name, List<string> usernames)
    {
        var users = new List<User> {await _userService.GetUserByContextAsync(Context)};        
        foreach (var username in usernames)
        {
            var user = await _userService.TryGetUserByUsernameAsync(username);
            if (user is null)
                continue;
            users.Add(user);
        }

        var chat = new Chat
        {
            Name = name,
            Users = users,
            Messages = new List<Message>()
        };
        _chatAppContext.Chats.Add(chat);
        await _chatAppContext.SaveChangesAsync();
        foreach (var user in users)
        {
            await _chatService.InviteUserToChatAsync(Clients, user, chat);
        }
    }

    public async Task<DtoChat> JoinChat(int chatId)
    {
        var chat = await _chatService.GetChatByIdWithAsocsAsync(chatId);
        var user = await _userService.GetUserByContextAsync(Context);

        if (!chat.Users.Contains(user))
            throw new BadRequestException(Errors.UserNotInChat);
        
        await _chatAppContext.SaveChangesAsync();
        
        await Groups.AddToGroupAsync(Context.ConnectionId, _chatService.GetUniqueChatName(chatId));

        return _dtoFactory.CreateDtoChat(chat);
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
    
    public async Task LeaveChat(int chatId)
    {
        var user = _userService.GetUserByContextWithChats(Context);

        var chat = user.Chats.FirstOrDefault(x => x.ChatId == chatId);
        
        if (chat is null)
            throw new BadRequestException(Errors.UserNotInChat);
        
        if(!user.Chats.Contains(chat))
            throw new BadRequestException(Errors.UserNotInChat);
        
        user.Chats.Remove(chat);

        await Clients.Group(user.Username).SendAsync("ReceiveKick", chatId);
        
        await _chatAppContext.SaveChangesAsync();
    }

    public async Task KickChat(int chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, _chatService.GetUniqueChatName(chatId));
    }

    public async Task<DtoUser> GetCurrentUser()
    {
        var user = await _userService.GetUserByContextAsync(Context);
        return new DtoUser(user.Username, user.PicturePath);
    }

    public async Task AddUserToChat(int chatId, string username)
    {
        var chat = await _chatService.GetChatByIdWithAsocsAsync(chatId);
        var user = await _userService.GetUserByUsernameAsync(username);
        chat.Users.Add(user);
        await _chatAppContext.SaveChangesAsync();
        await _chatService.InviteUserToChatAsync(Clients, user, chat);
    }
}