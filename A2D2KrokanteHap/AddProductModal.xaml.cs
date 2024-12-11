namespace A2D2KrokanteHap;

public partial class AddProductModal : ContentPage
{
	public AddProductModal()
	{
		InitializeComponent();
	}


    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    
    private async void SelectProductButton_Clicked(object sender, EventArgs e)
    {
        // Todo
        await Navigation.PopModalAsync();
    }
    
}