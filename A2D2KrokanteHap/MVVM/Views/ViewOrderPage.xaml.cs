using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class ViewOrderPage : ContentPage
{
	public ViewOrderPage(int Id)
	{
		InitializeComponent();
        BindingContext = new ViewOrderViewModel(Id);

    }
}