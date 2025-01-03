using A2D2KrokanteHap.MVVM.Models;
using Bogus;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class OrdersViewModel
    {
        public List<Order>? Orders { get; set; }

        public ICommand? ViewOrderCommand { get; set; }

        public OrdersViewModel()
        {
            Refresh();

            ViewOrderCommand = new Command(async () =>
            {
                // Command implementation
            });
        }

        // Sample products
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

        private void Refresh()
        {
            Orders = App.OrderRepo.GetEntities();

            Orders.Add(new Order
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
            });

            Orders.Add(new Order
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
                    }
                }
            });

        }
    }
}
