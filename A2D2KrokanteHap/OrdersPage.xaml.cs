namespace A2D2KrokanteHap;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
	{
		InitializeComponent();
	}

    private async void ViewOrderButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewOrderPage());
    }

    private async void ReorderButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReorderPage());
    }
}