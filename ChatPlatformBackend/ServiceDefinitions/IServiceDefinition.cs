namespace ChatPlatformBackend.ServiceDefinitions;

public interface IServiceDefinition
{
    public void DefineServices(IServiceCollection services, WebApplicationBuilder builder);
}