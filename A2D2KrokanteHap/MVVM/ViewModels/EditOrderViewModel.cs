
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
                int OrderId = App.OrderRepo.SaveEntityWithChildren(CurrentOrder, recursive: true);
                await Application.Current.MainPage.Navigation.PushAsync(new ConfirmOrderPage(OrderId, true));
            });

            IncreaseProductAmountCommand = new Command<int>((orderLineId) =>
            {
                var orderLine = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

                if (orderLine != null)
                {
                    orderLine.Amount++;
                    App.OrderLineRepo.SaveEntity(orderLine);
                    CalculateTotalPrice();
                }
                App.OrderLineRepo.SaveEntity(orderLine);
            });

            DecreaseProductAmountCommand = new Command<int>((orderLineId) =>
            {
                var orderLine = CurrentOrder?.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

                orderLine.Amount--;
                if (orderLine.Amount <= 0)
                {
                    CurrentOrder.OrderLines.Remove(orderLine);
                    App.OrderRepo.SaveEntityWithChildren(CurrentOrder);
                    App.OrderLineRepo.DeleteEntity(orderLine);

                }
                else
                {
                    App.OrderLineRepo.SaveEntity(orderLine);

                }
                CalculateTotalPrice();
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

            App.OrderRepo.SaveEntityWithChildren(CurrentOrder, true);
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = CurrentOrder.OrderLines?.Where(ol => ol?.Product != null)
                             .Sum(ol => ol.Product.Price * ol.Amount) ?? 0;
        }
    }
}
