namespace ChatPlatformBackend.ServiceDefinitions;

public class CorsServiceDefinition : IServiceDefinition
{
    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("https://localhost:3000")
                    .WithHeaders("Content-Type", "x-signalr-user-agent");
            });
        });
    }
}