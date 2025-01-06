
using A2D2KrokanteHap.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditOrderViewModel
    {
            public Order? CurrentOrder { get; set; }
            public double TotalPrice { get; set; }


            public ICommand? UpdateOrderCommand { get; set; }
            public ICommand? CancelEditCommand { get; set; }

            public EditOrderViewModel(int Id)
            {
                CurrentOrder = App.OrderRepo.GetEntityWithChildren(Id);

                CalculateTotalPrice();

            UpdateOrderCommand = new Command(async () =>
                {
                    // Todo - navigate back to orderCreation
                });

            CancelEditCommand = new Command(async () =>
                {
                    // Todo - navigate to order completion page
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
