using Microsoft.Maui.Controls;
using Microsoft.Maui.Maps;

namespace GpsApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
        LocationTxt.Text = await GetCachedLocation();

		SemanticScreenReader.Announce(LocationTxt.Text);
	}

    private async Task<string> GetCachedLocation() {
        try {
            Location location = await Geolocation.Default.GetLastKnownLocationAsync();

            if (location != null) {
                return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
            }
        //} catch (FeatureNotSupportedException fnsEx) {
            // Handle not supported on device exception
        //} catch (FeatureNotEnabledException fneEx) {
            // Handle not enabled on device exception
        //} catch (PermissionException pEx) {
            // Handle permission exception
        } catch (Exception ex) {
            return ex.Message;
            // Unable to get location
        }

        return "None";
    }
}

