using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;

namespace ChatPlatformBackend.ServiceDefinitions;

public class AuthenticationServiceDefinition : IServiceDefinition
{
    public void DefineServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(UTF8.GetBytes(builder.Configuration.GetSection("JwtSecret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["X-Access-Token"];
                        return Task.CompletedTask;
                    }
                };
            });
    }
}