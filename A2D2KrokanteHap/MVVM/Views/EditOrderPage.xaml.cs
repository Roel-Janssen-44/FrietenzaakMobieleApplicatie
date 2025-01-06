using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class EditOrderPage : ContentPage
{
	public EditOrderPage(int Id)
	{
		InitializeComponent();
        BindingContext = new EditOrderViewModel(Id);
    }
}