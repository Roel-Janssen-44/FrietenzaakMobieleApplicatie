using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class ConfirmOrderPage : ContentPage
{
	public ConfirmOrderPage(int Id)
	{
        InitializeComponent();
        BindingContext = new ConfirmOrderViewModel(Id);
    }
    public ConfirmOrderPage(int Id, bool CreateNewOrder)
	{
        InitializeComponent();
        BindingContext = new ConfirmOrderViewModel(Id, CreateNewOrder);
    }
}