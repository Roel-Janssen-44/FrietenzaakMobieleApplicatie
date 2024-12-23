using Frietzaak.Server.Models;
using System.Collections.ObjectModel;

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
        Product product3 = new Product
        {
            Id = 3,
            Name = "Frikandel",
            Category = "Snacks",
            Price = 2.75,
            Image = "snacks.jpg"
        };

        CurrentOrder = new Order
        {
            Id = 1,
            DateTime = DateTime.Now,
            EstimatedCompletionTime = DateTime.Now.AddMinutes(20),
            Completed = false,
            OrderLines = new ObservableCollection<OrderLine>

            //OrderLines = new List<OrderLine>
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
        };

        Test.ItemsSource = CurrentOrder.OrderLines;

    }


    private void CalculateTotal()
    {
        double total = CurrentOrder.CalculateTotalAmount(CurrentOrder);
        OrderTotal.Text = "Totaal: " + ConvertToCurrency(total);
    }


    public string ConvertToCurrency(double price)
    {
        // Check if the price is an integer (whole number)
        if (price % 1 == 0)
        {
            // If the price is an integer, display without decimal places and add the '-'
            return $"€ {price:0},-";
        }
        else
        {
            // Otherwise, format the price with two decimal places
            return $"€ {price.ToString("F2").Replace(".", ",")}";
        }
    }



    private void IncreaseProductAmount_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int orderLineId = (int)button.CommandParameter;

        var orderLine = CurrentOrder.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

        if (orderLine != null)
        {
            orderLine.Amount++;
            CalculateTotal();
        }
    }

    private async void DecreaseProductAmount_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        button.IsEnabled = false;

        try
        {
            int orderLineId = (int)button.CommandParameter;
            var orderLine = CurrentOrder.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

            if (orderLine != null)
            {
                orderLine.Amount--;
                if (orderLine.Amount <= 0)
                {
                    CurrentOrder.OrderLines.Remove(orderLine);
                }
                CalculateTotal();
            }
        }
        finally
        {
            button.IsEnabled = true; // Re-enable the button
        }
    }

    
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
        var addProductModal = new AddProductModal(OnProductSelected);
        await Navigation.PushModalAsync(addProductModal);
    }

    // This method will be called when a product is selected in the modal
    private void OnProductSelected(Product selectedProduct)
    {
        Console.WriteLine($"Newly added product: {selectedProduct.Name}");

        var newOrderLine = new OrderLine
        {
            Id = CurrentOrder.OrderLines.Count + 1,
            ProductId = selectedProduct.Id,
            Amount = 1,
            Product = selectedProduct,
        };

        CurrentOrder.OrderLines.Add(newOrderLine);
        CalculateTotal();
    }
}
