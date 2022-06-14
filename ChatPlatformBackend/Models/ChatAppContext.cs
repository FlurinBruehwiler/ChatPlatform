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
    
    public DbSet<User> Users { get; set; }
    public DbSet<GroupChat> GroupChats { get; set; }
    public DbSet<PrivateChat> PrivateChats { get; set; }
    public DbSet<Message> Messages { get; set; }
    
}