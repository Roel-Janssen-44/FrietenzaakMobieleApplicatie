
using A2D2KrokanteHap.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateOrderViewModel
    {

        Product product1 = new Product
        {
            Id = 1,
            Name = "Kleine friet",
            Category = "Friet",
            Price = 3.00,
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


        public Order? CurrentOrder{ get; set; } = new Order
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
                        Product = new Product
                        {
                            Id = 1,
                            Name = "Kleine friet",
                            Category = "Friet",
                            Price = 3.00,
                            Image = "friet.jpg"
                        }
                    },
                    new OrderLine
                    {
                        Id = 2,
                        ProductId = 2,
                        Amount = 1,
                        Product = new Product
                        {
                            Id = 2,
                            Name = "Grote friet",
                            Category = "Friet",
                            Price = 3.50,
                            Image = "friet.jpg"
                        }
                    },
                    new OrderLine
                    {
                        Id = 3,
                        ProductId = 3,
                        Amount = 1,
                        Product = new Product
                        {
                            Id = 3,
                            Name = "Frikandel",
                            Category = "Snacks",
                            Price = 2.75,
                            Image = "snacks.jpg"
                        }
                    }
                }
            };


        public ICommand? TestCommand { get; set; }


        public CreateOrderViewModel()
        {
            
            TestCommand = new Command(async () =>
            {
            });

        }

        //private async void AddProductButton_Clicked(object sender, EventArgs e)
        //{
        //    var addProductModal = new AddProductModal(OnProductSelected);
        //    await Navigation.PushModalAsync(addProductModal);
        //}


    }
}
