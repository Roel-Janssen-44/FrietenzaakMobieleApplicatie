
using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.MVVM.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ConfirmOrderViewModel
    {
        public Order? CurrentOrder { get; set; }
        public double TotalPrice { get; set; }


        public ICommand? EditOrderCommand { get; set; }
        public ICommand? ConfirmOrderCommand { get; set; }

        public ConfirmOrderViewModel(int Id)
        {
            CurrentOrder = App.OrderRepo.GetEntityWithChildren(Id);

            CalculateTotalPrice();


            EditOrderCommand = new Command(async () =>
            {
                // Todo - navigate back to orderCreation
            });

            ConfirmOrderCommand = new Command(async () =>
            {
                CurrentOrder.Completed = true;
                App.OrderRepo.SaveEntityWithChildren(CurrentOrder, recursive: true);

                await Application.Current.MainPage.Navigation.PushAsync(new OrderPlacedPage(CurrentOrder.Id));

            });
        }
        public ConfirmOrderViewModel(int Id, bool CreateNewOrder)
        {
            CurrentOrder = App.OrderRepo.GetEntityWithChildren(Id);

            CalculateTotalPrice();


            EditOrderCommand = new Command(async () =>
            {
                // Todo - navigate back to orderCreation
            });

            ConfirmOrderCommand = new Command(async () =>
            {
                CurrentOrder.Completed = true;
                CurrentOrder.Id = 0;

                int NewOrderId = App.OrderRepo.SaveEntityWithChildren(CurrentOrder, recursive: true);

                await Application.Current.MainPage.Navigation.PushAsync(new OrderPlacedPage(NewOrderId));

            });
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = CurrentOrder.OrderLines?.Where(ol => ol?.Product != null)
                             .Sum(ol => ol.Product.Price * ol.Amount) ?? 0;
        }



    }
}
