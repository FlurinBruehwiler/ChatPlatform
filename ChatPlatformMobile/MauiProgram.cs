using ChatPlatformMobile.Pages;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ChatPlatformMobile;

public static class MauiProgram
{
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

        builder.Services.AddSingleton<WelcomePage>();
        builder.Services.AddSingleton<WelcomeViewModel>();

        builder.Services.AddTransient<CreateChatPage>();
        builder.Services.AddTransient<CreateChatViewModel>();

        builder.Services.AddTransient<ChatPage>();
        builder.Services.AddTransient<ChatViewModel>();

        builder.Services.AddHttpClient();
        
        builder.Services.AddSingleton<SyncService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}