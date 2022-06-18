using ChatPlatformBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.AddServices();

var app = builder.Build();

app.UseEndpointDefinitions();

app.UseAuthentication();
app.UseAuthorization();

app.Run();