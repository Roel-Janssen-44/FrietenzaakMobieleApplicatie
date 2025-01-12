
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
            CustomerId = Preferences.Get("LoggedInUserId", -1),
            Customer = App.CustomerRepo.GetEntity(Preferences.Get("LoggedInUserId", -1)),
            DateTime = DateTime.Now,
            EstimatedCompletionTime = DateTime.Now.AddMinutes(20),
            Completed = false,
            OrderLines = new ObservableCollection<OrderLine>{}
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
            var CurrentCustomer = App.CustomerRepo.GetEntity(Preferences.Get("LoggedInUserId", -1));

            var customerId = Preferences.Get("LoggedInUserId", -1);
            CurrentOrder.Customer = CurrentCustomer;
            CurrentOrder.CustomerId = CurrentCustomer.Id;

            int OrderId = App.OrderRepo.SaveEntity(CurrentOrder);
            CurrentOrder.Id = OrderId;

            IncreaseProductAmountCommand = new Command<int>((orderLineId) =>
            {
                var orderLine = App.OrderLineRepo.GetEntityWithChildren(orderLineId);
                if (orderLine != null)
                {
                    orderLine.Amount++;

                    App.OrderLineRepo.SaveEntity(orderLine);
                }
                 
                var orderLineFromCurrentOrder = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

                if (orderLineFromCurrentOrder != null)
                {
                    orderLineFromCurrentOrder.Amount++;
                }

                CalculateTotal();
            });



            DecreaseProductAmountCommand = new Command<int>((orderLineId) =>
            {
                // Fetch the order line from the database
                var orderLine = App.OrderLineRepo.GetEntityWithChildren(orderLineId);
                if (orderLine != null)
                {
                    // Decrease the amount
                    orderLine.Amount--;

                    if (orderLine.Amount <= 0)
                    {
                        // Remove from both database and current order if the amount is 0 or less
                        App.OrderLineRepo.DeleteEntity(orderLine);
                        var orderLineFromCurrentOrder = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

                        if (orderLineFromCurrentOrder != null)
                        {
                            CurrentOrder.OrderLines.Remove(orderLineFromCurrentOrder);
                        }
                    }
                    else
                    {
                        // Update the database with the new amount
                        App.OrderLineRepo.SaveEntity(orderLine);

                        // Update the in-memory order line
                        var orderLineFromCurrentOrder = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

                        if (orderLineFromCurrentOrder != null)
                        {
                            orderLineFromCurrentOrder.Amount--;
                        }
                    }

                    // Recalculate the total price
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
                //var createdOrderId = App.OrderRepo.SaveEntityWithChildren(CurrentOrder);
                await Application.Current.MainPage.Navigation.PushAsync(new ConfirmOrderPage(CurrentOrder.Id));
            });

            CalculateTotal();
        }


        private void OnProductSelected(Product selectedProduct)
        {
            var newOrderLine = new OrderLine
            {
                Id = 0,
                ProductId = selectedProduct.Id,
                OrderId = CurrentOrder.Id,
                Amount = 1,
                Product = selectedProduct,
            };

            int OrderLineId = App.OrderLineRepo.SaveEntityWithChildren(newOrderLine);

            newOrderLine.Id = OrderLineId;
            CurrentOrder.OrderLines.Add(newOrderLine);

            //App.OrderRepo.SaveEntityWithChildren(CurrentOrder, false);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalPrice = CurrentOrder?.OrderLines.Sum(ol => ol.Amount * ol.Product.Price) ?? 0;
        }


    }
}
