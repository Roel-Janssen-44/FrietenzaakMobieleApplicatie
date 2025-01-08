
//using Microsoft.Maui.Essentials; // For Essentials APIs
using System;

namespace A2D2KrokanteHap.MVVM.Views;
public partial class MapsPage : ContentPage
{

    private bool isFlashlightOn = false; // Track flashlight status


    public MapsPage()
	{
		InitializeComponent();

    }
    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        // Specify the target location
        var location = new Location(47.645160, -122.1306032);
        var options = new MapLaunchOptions
        {
            Name = "Microsoft Building 25"
        };

        try
        {
            // Open the map app with the location
            await Map.Default.OpenAsync(location, options);
        }
        catch (Exception ex)
        {
            // Handle exceptions, e.g., if no map app is available
            await DisplayAlert("Error", "Unable to open the map application.", "OK");
        }
    }

    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        try
        {
            // Request location permission
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                // Get the device's current location
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(10)
                });

                if (location != null)
                {
                    LocationLabel.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
                }
                else
                {
                    LocationLabel.Text = "Location not available.";
                }
            }
            else
            {
                LocationLabel.Text = "Permission denied.";
            }
        }
        catch (Exception ex)
        {
            LocationLabel.Text = $"Error: {ex.Message}";
        }
    }


    private void OnStartCompassClicked(object sender, EventArgs e)
    {
        try
        {
            if (!Compass.IsMonitoring)
            {
                // Set the reading interval to 100ms
                Compass.Start(SensorSpeed.UI);
                Compass.ReadingChanged += Compass_ReadingChanged;
            }
        }
        catch (FeatureNotSupportedException)
        {
            CompassLabel.Text = "Compass not supported on this device.";
        }
    }

    // Stop Compass
    private void OnStopCompassClicked(object sender, EventArgs e)
    {
        if (Compass.IsMonitoring)
        {
            Compass.Stop();
            Compass.ReadingChanged -= Compass_ReadingChanged;
            CompassLabel.Text = "Compass stopped.";
        }
    }

    // Update UI when the reading changes
    private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
    {
        // Get the magnetic heading
        double heading = e.Reading.HeadingMagneticNorth;
        CompassLabel.Text = $"Heading: {heading:F1}°";
    }

    private async void OnToggleFlashlightClicked(object sender, EventArgs e)
    {
        try
        {
            if (isFlashlightOn)
            {
                // Turn off the flashlight
                await Flashlight.TurnOffAsync();
                FlashlightStatusLabel.Text = "Flashlight is OFF";
            }
            else
            {
                // Turn on the flashlight
                await Flashlight.TurnOnAsync();
                FlashlightStatusLabel.Text = "Flashlight is ON";
            }

            // Toggle state
            isFlashlightOn = !isFlashlightOn;
        }
        catch (FeatureNotSupportedException)
        {
            // If flashlight is not supported
            await DisplayAlert("Error", "Flashlight not supported on this device.", "OK");
        }
        catch (PermissionException)
        {
            // If permissions are denied
            await DisplayAlert("Error", "Permission denied to access the flashlight.", "OK");
        }
        catch (Exception ex)
        {
            // Handle any other exceptions
            await DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
        }
    }


    private void OnShortBuzzClicked(object sender, EventArgs e)
    {
        try
        {
            Vibration.Vibrate(); // Default short vibration
        }
        catch (FeatureNotSupportedException)
        {
            DisplayAlert("Error", "Vibration not supported on this device.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
        }
    }

    // Long Vibration (e.g., 1 second)
    private void OnLongBuzzClicked(object sender, EventArgs e)
    {
        try
        {
            TimeSpan duration = TimeSpan.FromSeconds(1); // 1 second
            Vibration.Vibrate(duration);
        }
        catch (FeatureNotSupportedException)
        {
            DisplayAlert("Error", "Vibration not supported on this device.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
        }
    }

    // Stop Vibration (if supported)
    private void OnStopBuzzClicked(object sender, EventArgs e)
    {
        try
        {
            Vibration.Cancel(); // Stops ongoing vibration
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
        }
    }

}