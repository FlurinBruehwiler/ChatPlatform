using ChatPlatformBackend.EndpointDefinitions;
using ChatPlatformBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.AddServices(typeof(IEndpointDefinition));

var app = builder.Build();
app.UseEndpointDefinitions();

app.UseAuthentication();
app.UseAuthorization();

app.Run();