namespace ChatPlatformBackend.EndpointDefinitions;

public interface IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app);
}