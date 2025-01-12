namespace A2D2KrokanteHap.MVVM.Views;

public partial class SignOut : ContentPage
{
    public SignOut()
    {
        InitializeComponent();
    }

    public void Logout()
    {
        Preferences.Set("IsLoggedIn", false);
        Preferences.Set("LoggedInUser", null);
        Preferences.Set("LoggedInUserId", -1);
    }

    private void OnSignOutClicked(object sender, EventArgs e)
    {
        Logout();

        // Navigate to the login page or perform additional cleanup
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}
