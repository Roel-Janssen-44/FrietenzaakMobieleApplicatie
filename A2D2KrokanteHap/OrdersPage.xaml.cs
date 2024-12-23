using Frietzaak.Server.Models;
using System.Collections.ObjectModel;

namespace A2D2KrokanteHap;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
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
        Product product3 = new Product
        {
            Id = 3,
            Name = "Frikandel",
            Category = "Snacks",
            Price = 2.75,
            Image = "snacks.jpg"
        };

        List<Order> Orders = new List<Order>
        {
            new Order
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
                        ProductId = 1,
                        Amount = 2,
                        Product = product1
                    },
                    new OrderLine
                    {
                        Id = 2,
                        ProductId = 2,
                        Amount = 1,
                        Product = product2
                    },
                    new OrderLine
                    {
                        Id = 3,
                        ProductId = 3,
                        Amount = 1,
                        Product = product3
                    }
                }
            },
            new Order
            {
                Id = 2,
                DateTime = DateTime.Now,
                EstimatedCompletionTime = DateTime.Now.AddMinutes(80),
                Completed = false,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Id = 1,
                        ProductId = 1,
                        Amount = 2,
                        Product = product1
                    },
                }
            }
        };



        OrdersBinding.ItemsSource = Orders;

    }

    private async void ViewOrderButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewOrderPage());
    }

    private async void ReorderButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReorderPage());
    }
    //private async void ViewOrderButton_Clicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    int orderId = (int)button.CommandParameter;

    //    // Navigate to order details page
    //    await Navigation.PushAsync(new ViewOrderPage(orderId)); // Pass the ID to view order details
    //}

    //private async void ReorderButton_Clicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    int orderId = (int)button.CommandParameter;

    //    // Navigate to reorder page
    //    await Navigation.PushAsync(new ReorderPage(orderId)); // Pass the ID to reorder
    //}

}