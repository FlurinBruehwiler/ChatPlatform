using ChatPlatformMobile.Pages;

namespace ChatPlatformMobile;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
        Routing.RegisterRoute(nameof(ChatOverviewPage), typeof(ChatOverviewPage));
        Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
        Routing.RegisterRoute(nameof(CreateChatPage), typeof(CreateChatPage));
        Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
    }
}