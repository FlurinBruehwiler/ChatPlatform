using ChatPlatformBackend;
using ChatPlatformBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.AddServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins("https://localhost:3000")
            .WithHeaders("Content-Type");
    });
});

var app = builder.Build();
app.UseOptions();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpointDefinitions();


app.Run();