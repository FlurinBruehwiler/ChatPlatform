using ChatPlatformBackend.DtoModels;
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
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost( "/register", async (DtoUser dtoUser, IAuthService authService, ChatAppContext chatAppContext, HttpResponse httpResponse) =>
{
    authService.CreatePasswordHash(dtoUser.Password, out var passwordHash, out var passwordSalt);
    var user = new User
    {
        Username = dtoUser.Username,
        PasswordHash = passwordHash,
        PasswordSalt = passwordSalt
    };
    chatAppContext.Users.Add(user);
    await chatAppContext.SaveChangesAsync();
    httpResponse.Cookies.Append("X-Access-Token", authService.CreateToken(user), new CookieOptions
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Strict
    });

    return Results.Ok();
});

app.MapPost( "/login", async (DtoUser dtoUser, IAuthService authService, IUserService userService, HttpResponse httpResponse) =>
{
    var user = await userService.GetUserByUsernameAsync(dtoUser.Username);

    if (!authService.VerifyPasswordHash(dtoUser.Password, user.PasswordHash, user.PasswordSalt))
        return Results.BadRequest("Wrong Password");
    
    httpResponse.Cookies.Append("X-Access-Token", authService.CreateToken(user), new CookieOptions
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Strict
    });

    return Results.Ok();
});

app.MapHub<ChatHub>("/chatHub");
app.Run();