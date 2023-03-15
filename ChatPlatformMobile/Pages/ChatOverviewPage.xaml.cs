namespace ChatPlatformMobile.Pages;

public partial class ChatOverviewPage
{
    public ChatOverviewPage(ChatOverviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}