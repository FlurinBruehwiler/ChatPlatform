using ChatPlatformBackend;
using ChatPlatformBackend.Extensions;
using ChatPlatformBackend.Filters;
using ChatPlatformBackend.Hubs;
using ChatPlatformBackend.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.AddFilter<GroupFilter>();
});

builder.AddServices();


var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider
//         .GetRequiredService<ChatAppContext>();
//     
//     dbContext.Database.Migrate();
// }

app.UseStaticFiles();

app.UseOptions();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpointDefinitions();

app.MapHub<ChatHub>("/chatHub");


app.Run();