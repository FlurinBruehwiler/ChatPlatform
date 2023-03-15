using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;

namespace ChatPlatformBackend.ServiceDefinitions;

public class AuthenticationServiceDefinition : IServiceDefinition
{
    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                        var cookieName = builder.Configuration.GetSection("CookieName").Value;

                        if (cookieName is null)
                            throw new Exception("Could not find 'CookieName' in configuration");

                        if (context.Request.Cookies.TryGetValue(cookieName, out var cookieToken))
                        {
                            context.Token = cookieToken;
                            return Task.CompletedTask;
                        }

                        if (context.Request.Headers.TryGetValue("Authorization", out var headerToken))
                        {
                            context.Token = headerToken;
                            return Task.CompletedTask;
                        }
                        
                        context.Token = string.Empty;
                        return Task.CompletedTask;
                    }
                };
            });
    }
}