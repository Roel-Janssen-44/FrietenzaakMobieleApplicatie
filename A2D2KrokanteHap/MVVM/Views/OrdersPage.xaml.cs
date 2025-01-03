using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
	{
		InitializeComponent();
        BindingContext = new OrdersViewModel();
    }
}