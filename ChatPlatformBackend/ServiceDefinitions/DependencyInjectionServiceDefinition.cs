using ChatPlatformBackend.Services.Implementations;
using ChatPlatformBackend.Services.Interfaces;

namespace ChatPlatformBackend.ServiceDefinitions;

public class DependencyInjectionServiceDefinition : IServiceDefinition
{
    public void DefineServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
    }
}