

using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.MVVM.Views;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class OrderPlacedViewModel
    {
        public int OrderId { get; set; }


        public ICommand? NavigateBackCommand { get; set; }

        public OrderPlacedViewModel(int Id)
        {
            OrderId = Id;

            NavigateBackCommand = new Command(async () =>
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new OrdersPage());
            });
        }

    }
}
