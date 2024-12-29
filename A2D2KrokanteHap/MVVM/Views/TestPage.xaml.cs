using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;
public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
        BindingContext = new TestPageViewModel();

    }
}
