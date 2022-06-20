using ChatPlatformBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.AddServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:3000", 
                "https://localhost:3000", 
                "http://127.0.0.1:3000", 
                "https://127.0.0.1:3000")
            .WithHeaders("Access-Control-Allow-Headers", 
                "Content-Type");
    });
});

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpointDefinitions();


app.Run();