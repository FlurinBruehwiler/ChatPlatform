using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPlatformMobile.Pages;

public partial class WelcomePage
{
    public WelcomePage(WelcomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}