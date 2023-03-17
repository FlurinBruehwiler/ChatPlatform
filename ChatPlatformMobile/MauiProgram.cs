using ChatPlatformMobile.Pages;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ChatPlatformMobile;

public static class MauiProgram
{
    private static SyncService _syncService;
    
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<ChatOverviewPage>();
        builder.Services.AddTransient<ChatOverviewViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainViewModel>();

        builder.Services.AddTransient<AuthPage>();
        builder.Services.AddTransient<AuthViewModel>();

        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<WelcomeViewModel>();

        builder.Services.AddTransient<CreateChatPage>();
        builder.Services.AddTransient<CreateChatViewModel>();

        builder.Services.AddTransient<ChatPage>();
        builder.Services.AddTransient<ChatViewModel>();

        builder.Services.AddHttpClient();
        
        builder.Services.AddTransient(provider =>
        {
            if (_syncService is not null)
                return _syncService;
            
            _syncService = new SyncService(provider.GetRequiredService<DtoMapper>());

            return _syncService;
        });
        builder.Services.AddSingleton<DtoMapper>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    
    public static void DeleteSyncService()
    {
        _syncService = null;
    }
}