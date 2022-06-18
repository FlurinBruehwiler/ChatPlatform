using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.Models;

public class ChatAppContext : DbContext
{
    public ChatAppContext()
    {
        
    }
    
    public ChatAppContext(DbContextOptions<ChatAppContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
}