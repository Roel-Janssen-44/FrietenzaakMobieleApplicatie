using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class CreateOrderPage : ContentPage
{
	public CreateOrderPage()
	{
        InitializeComponent();
        BindingContext = new CreateOrderViewModel();

    }
}
