namespace ChatPlatformMobile.Pages;

public partial class CreateChatPage
{
    public CreateChatPage(CreateChatViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}