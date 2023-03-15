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

        builder.Services.AddSingleton<ChatOverviewPage>();
        builder.Services.AddSingleton<ChatOverviewViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<AuthPage>();
        builder.Services.AddSingleton<AuthViewModel>();

        builder.Services.AddSingleton<WelcomePage>();
        builder.Services.AddSingleton<WelcomeViewModel>();

        builder.Services.AddSingleton<CreateChatPage>();
        builder.Services.AddSingleton<CreateChatViewModel>();
        
        builder.Services.AddSingleton<SyncService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}