
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;


namespace A2D2KrokanteHap;

public partial class BottomNavigation : Shell
{
    public BottomNavigation()
    {
        InitializeComponent();

        this.Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = Colors.Transparent,
            StatusBarStyle = StatusBarStyle.DarkContent
        });
    }
}
