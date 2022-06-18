using ChatPlatformBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatPlatformBackend.ServiceDefinitions;

public class DatabaseServiceDefinition : IServiceDefinition
{
    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ChatAppContext>(options =>
        {
            var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
        });
    }
}