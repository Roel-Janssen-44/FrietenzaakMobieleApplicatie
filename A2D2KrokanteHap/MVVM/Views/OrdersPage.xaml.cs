using A2D2KrokanteHap.MVVM.ViewModels;

namespace A2D2KrokanteHap.MVVM.Views;

public partial class OrdersPage : ContentPage
{
    private OrdersViewModel _viewModel;

    public OrdersPage()
    {
        InitializeComponent();

        // Initialize and set BindingContext
        _viewModel = new OrdersViewModel();
        BindingContext = _viewModel;
    }

    // Override OnAppearing to refresh the data when the page is revisited
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Call Refresh to reload the orders
        _viewModel.Refresh();
    }
}
