using ChatPlatformBackend.EndpointDefinitions;
using ChatPlatformBackend.ServiceDefinitions;

namespace ChatPlatformBackend.Extensions;

public static class WebApplicationExtensions
{
    public static void UseEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var endpointDefinition in definitions)
        {
            endpointDefinition.DefineEndpoints(app);
        }
    }
    
    public static void AddServices(
        this WebApplicationBuilder builder, params Type[] scanMarkers)
    {
        var serviceDefinitions = new List<IServiceDefinition>();

        foreach (var marker in scanMarkers)
        {
            serviceDefinitions.AddRange(
                marker.Assembly.ExportedTypes
                    .Where(x => typeof(IServiceDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IServiceDefinition>()
            );
        }

        foreach (var endpointDefinition in serviceDefinitions)
        {
            endpointDefinition.DefineServices(builder.Services, builder);
        }

        if (serviceDefinitions is IReadOnlyCollection<IServiceDefinition> readOnlyCollection)
        {
            builder.Services.AddSingleton(readOnlyCollection);
        }
    }
}