
using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;
public partial class OrderPlacedPage : ContentPage
{
	public OrderPlacedPage(int Id)
	{
        InitializeComponent();
        BindingContext = new OrderPlacedViewModel(Id);
        NavigationPage.SetHasBackButton(this, false);

    }
} 