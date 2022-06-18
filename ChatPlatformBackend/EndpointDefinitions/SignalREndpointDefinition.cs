using ChatPlatformBackend.Hubs;

namespace ChatPlatformBackend.EndpointDefinitions;

public class SignalREndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapHub<ChatHub>("/chatHub");
    }
}