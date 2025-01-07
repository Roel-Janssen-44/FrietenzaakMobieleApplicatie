using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

        this.Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = Colors.Transparent,
            StatusBarStyle = StatusBarStyle.DarkContent
        });
    }
}