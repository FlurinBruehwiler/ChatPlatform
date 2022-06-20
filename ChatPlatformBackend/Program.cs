using ChatPlatformBackend;
using ChatPlatformBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.AddServices();

var app = builder.Build();

app.UseOptions();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpointDefinitions();

app.Run();