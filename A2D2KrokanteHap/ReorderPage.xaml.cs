namespace A2D2KrokanteHap;

public partial class ReorderPage : ContentPage
{
    public ReorderPage()
    {
        InitializeComponent();
    }



    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void ReorderButton_Clicked(object sender, EventArgs e)
    {
        // Todo - go to next page
        //await Navigation.PushAsync(new ViewOrderPage());
    }
    private async void AddProductButton_Clicked(object sender, EventArgs e)
    {
        // Todo - Show modal
        await Navigation.PushModalAsync(new AddProductModal());
    }
    
        
        

}