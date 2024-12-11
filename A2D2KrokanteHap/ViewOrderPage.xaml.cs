namespace A2D2KrokanteHap;

public partial class ViewOrderPage : ContentPage
{
	public ViewOrderPage()
	{
		InitializeComponent();
	}

    
    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    
}