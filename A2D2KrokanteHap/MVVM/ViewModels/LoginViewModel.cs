using A2D2KrokanteHap.MVVM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string? _username;
        private string? _password;
        private string? _message;

        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string? Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        // Login Command
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter username and password.";
                return;
            }

            // Use repository to check credentials
            var user = App.CustomerRepo!.GetByCondition(c => c.UserName == Username && c.Password == Password);

            if (user != null)
            {
                // Save login state, username, and user ID
                Preferences.Set("IsLoggedIn", true);
                Preferences.Set("LoggedInUser", user.UserName);
                Preferences.Set("LoggedInUserId", user.Id);
                //await Shell.Current.GoToAsync("//CreateOrderPage");
                Application.Current.MainPage = new BottomNavigation();
            }
            else
            {
                Message = "Invalid username or password.";
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
