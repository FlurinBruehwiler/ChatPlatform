using ChatPlatformMobile.Pages;

namespace ChatPlatformMobile;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
    }
}