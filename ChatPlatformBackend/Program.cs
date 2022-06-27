using ChatPlatformBackend;
using ChatPlatformBackend.Extensions;
using ChatPlatformBackend.Filters;
using ChatPlatformBackend.Hubs;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.AddFilter<GroupFilter>();
});

builder.AddServices();

var app = builder.Build();

app.UseOptions();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpointDefinitions();

app.MapHub<ChatHub>("/chatHub");


app.Run();