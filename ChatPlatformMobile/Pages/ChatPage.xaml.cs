namespace ChatPlatformMobile.Pages;

public partial class ChatPage
{
    public ChatPage(ChatViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}