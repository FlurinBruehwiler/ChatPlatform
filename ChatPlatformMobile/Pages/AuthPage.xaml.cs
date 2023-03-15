namespace ChatPlatformMobile.Pages;

public partial class AuthPage
{
    public AuthPage(AuthViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}