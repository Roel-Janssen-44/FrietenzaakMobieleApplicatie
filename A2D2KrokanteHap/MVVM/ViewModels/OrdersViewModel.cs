using A2D2KrokanteHap.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class OrdersViewModel
    {
        public List<Order>? Orders { get; set; }
        public List<Order>? CompletedOrders { get; set; }
        public List<Order>? DraftOrders { get; set; }

        public ICommand? ViewOrderCommand { get; set; }

        public OrdersViewModel()
        {
            Refresh();

            ViewOrderCommand = new Command(async () =>
            {
                // Command implementation
            });
        }


        private void Refresh()
        {
            Orders = App.OrderRepo.GetEntitiesWithChildren();
            DraftOrders = new List<Order>();
            CompletedOrders = new List<Order>();

            foreach (var order in Orders)
            {
                if (order.Completed)
                {
                    CompletedOrders.Add(order);
                } else
                {
                    DraftOrders.Add(order);
                }
            }
        }
    }
}   
