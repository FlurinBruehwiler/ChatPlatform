using ChatPlatformBackend.Hubs;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Implementations;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatAppContext>(options =>
{
    var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
    options.UseSqlite(connectionString);
    options.EnableSensitiveDataLogging();
});

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
                context.Token = context.Request.Cookies["X-Access-Token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSignalR();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
app.Run();