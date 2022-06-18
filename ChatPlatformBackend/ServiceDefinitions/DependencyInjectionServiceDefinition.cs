using ChatPlatformBackend.Services.Implementations;
using ChatPlatformBackend.Services.Interfaces;

namespace ChatPlatformBackend.ServiceDefinitions;

public class DependencyInjectionServiceDefinition : IServiceDefinition
{
    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IChatService, ChatService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
    }
}