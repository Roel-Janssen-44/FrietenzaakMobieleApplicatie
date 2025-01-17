﻿
using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.MVVM.Views;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditOrderViewModel
    {
        public Order? CurrentOrder { get; set; }

        private double _totalPrice;
        public double TotalPrice
        {
            get => _totalPrice;
            set => _totalPrice = value;
        }


        public ICommand? UpdateOrderCommand { get; set; }
        public ICommand? CancelEditCommand { get; set; }

        public ICommand? IncreaseProductAmountCommand { get; set; }
        public ICommand? DecreaseProductAmountCommand { get; set; }
        public ICommand? AddProductCommand { get; set; }


        public EditOrderViewModel(int Id)
        {
            CurrentOrder = App.OrderRepo.GetEntityWithChildren(Id);

            CalculateTotalPrice();

            CancelEditCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync() ;
            });

            UpdateOrderCommand = new Command(async () =>
            {
                int OrderId = App.OrderRepo.SaveEntityWithChildren(CurrentOrder);
                await Application.Current.MainPage.Navigation.PushAsync(new ConfirmOrderPage(OrderId, true));
            });


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

                CalculateTotalPrice();
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
                    CalculateTotalPrice();
                }
            });


            AddProductCommand = new Command(async () =>
            {
                var addProductModal = new AddProductModal(OnProductSelected);
                await Application.Current.MainPage.Navigation.PushModalAsync(addProductModal);
            });

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

            App.OrderRepo.SaveEntityWithChildren(CurrentOrder);
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = CurrentOrder.OrderLines?.Where(ol => ol?.Product != null)
                             .Sum(ol => ol.Product.Price * ol.Amount) ?? 0;
        }
    }
}
