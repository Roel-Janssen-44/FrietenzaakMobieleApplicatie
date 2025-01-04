
using A2D2KrokanteHap.MVVM.Models;
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

            //if (CurrentOrder.OrderLines != null)
            //{
            //    CurrentOrder.OrderLines.CollectionChanged += (s, e) => CalculateTotalPrice();
            //}

            EditOrderCommand = new Command(async () =>
            {
                // Todo - navigate back to orderCreation
            });

            ConfirmOrderCommand = new Command(async () =>
            {
                // Todo - navigate to order completion page
                CurrentOrder.Completed = true;
                App.OrderRepo.SaveEntityWithChildren(CurrentOrder, recursive: true);
            });
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = CurrentOrder.OrderLines?.Where(ol => ol?.Product != null)
                             .Sum(ol => ol.Product.Price * ol.Amount) ?? 0;
        }



    }
}
