using Frietzaak.Server.Models;

namespace A2D2KrokanteHap;

public partial class CreateOrderPage : ContentPage
{

    public Order CurrentOrder { get; set; }

    public CreateOrderPage()
	{
		InitializeComponent();

        Product product1 = new Product
        {
            Id = 1,
            Name = "Kleine friet",
            Category = "Friet",
            Price = 3.00,
            DiscountPrice = 2.50, 
            Image = "friet.jpg" 
        };
        Product product2 = new Product
        {
            Id = 2,
            Name = "Grote friet",
            Category = "Friet",
            Price = 3.50,
            Image = "friet.jpg" 
        };

        CurrentOrder = new Order
        {
            Id = 1,
            DateTime = DateTime.Now,
            EstimatedCompletionTime = DateTime.Now.AddMinutes(20),
            Completed = false,
            OrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    Id = 1,
                    ProductId = 101,
                    Amount = 2,
                    Product = product1
                },
                new OrderLine
                {
                    Id = 2,
                    ProductId = 102,
                    Amount = 1,
                    Product = product2
                }
            }
        };

        Test.ItemsSource = CurrentOrder.OrderLines;

    }


    // Todo - create order object


    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void OrderButton_Clicked(object sender, EventArgs e)
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