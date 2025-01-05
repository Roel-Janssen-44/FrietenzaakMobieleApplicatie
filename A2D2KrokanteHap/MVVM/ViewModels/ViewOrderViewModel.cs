using A2D2KrokanteHap.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewOrderViewModel
    {

        public Order? CurrentOrder{ get; set; }
        public ICommand? GoBackCommand { get; set; }
        public ICommand? LogoutCommand { get; set; }

        public ViewOrderViewModel(int Id)
        {
            CurrentOrder = App.OrderRepo.GetEntityWithChildren(Id);

            GoBackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync(); 
            });

        }


    }
}
