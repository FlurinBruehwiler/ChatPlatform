using ChatPlatformBackend.EndpointDefinitions;
using ChatPlatformBackend.ServiceDefinitions;

namespace ChatPlatformBackend.Extensions;

public static class WebApplicationExtensions
{
    public static void UseEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(IEndpointDefinition).Assembly.ExportedTypes
            .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IEndpointDefinition>().ToList();
            
        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineEndpoints(app);
        }
    }
    
    public static void AddServices(this WebApplicationBuilder builder)
    {
        var serviceDefinitions =
            typeof(IServiceDefinition).Assembly.ExportedTypes
                .Where(x => typeof(IServiceDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IServiceDefinition>().ToList();

        foreach (var serviceDefinition in serviceDefinitions)
        {
            serviceDefinition.DefineServices(builder);
        }
    }
}