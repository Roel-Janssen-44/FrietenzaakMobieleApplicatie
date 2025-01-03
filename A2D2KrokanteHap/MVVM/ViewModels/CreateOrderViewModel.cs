
using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.MVVM.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateOrderViewModel
    {


        public Order? CurrentOrder { get; set; } = new Order
        {
            Id = 0,
            DateTime = DateTime.Now,
            EstimatedCompletionTime = DateTime.Now.AddMinutes(20),
            Completed = false,
            OrderLines = new ObservableCollection<OrderLine>
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



        public ICommand? IncreaseProductAmountCommand { get; set; }
        public ICommand? DecreaseProductAmountCommand { get; set; }
        public ICommand? AddProductCommand { get; set; }
        public ICommand? CreateOrderCommand { get; set; }

        private double _totalPrice;
        public double TotalPrice
        {
            get => _totalPrice;
            set => _totalPrice = value;
        }

        public CreateOrderViewModel()
        {

            IncreaseProductAmountCommand = new Command<int>((orderLineId) =>
            {
                var orderLine = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);
                if (orderLine != null)
                {
                    orderLine.Amount++;
                    CalculateTotal();
                }
            });

            DecreaseProductAmountCommand = new Command<int>((orderLineId) =>
            {
                var orderLine = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);
                if (orderLine != null)
                {
                    orderLine.Amount--;
                    if (orderLine.Amount <= 0)
                    {
                        CurrentOrder.OrderLines.Remove(orderLine);
                    }
                    CalculateTotal();
                }
            });

            AddProductCommand = new Command(async () =>
            {
                var addProductModal = new AddProductModal(OnProductSelected);
                await Application.Current.MainPage.Navigation.PushModalAsync(addProductModal);
            });

            CreateOrderCommand = new Command(async () =>
            {
                var createdOrderId = App.OrderRepo.SaveEntity(CurrentOrder);
                await Application.Current.MainPage.Navigation.PushAsync(new ConfirmOrderPage(createdOrderId));
            });

            CalculateTotal();
        }


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

        private void CalculateTotal()
        {
            TotalPrice = CurrentOrder?.OrderLines.Sum(ol => ol.Amount * ol.Product.Price) ?? 0;
        }


    }
}
