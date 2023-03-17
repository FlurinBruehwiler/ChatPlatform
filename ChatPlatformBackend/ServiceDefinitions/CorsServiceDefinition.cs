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
                    .WithOrigins("https://localhost:3000", "http://localhost:3000", "https://chatplatform-production-5907.up.railway.app", "http://chatplatform-production-5907.up.railway.app")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
        });
    }
}