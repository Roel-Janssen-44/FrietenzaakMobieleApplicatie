namespace A2D2KrokanteHap;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private void OnLoginClick(object sender, EventArgs e)
    {
        Application.Current.MainPage = new BottomNavigation();
    }

    private void OnEntryTextChanged(object sender, EventArgs e)
    { }
    private void OnEntryCompleted(object sender, EventArgs e)
    { }

}