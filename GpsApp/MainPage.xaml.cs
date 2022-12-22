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
        //LocationTxt.Text = await GetCachedLocation();
        LocationTxt.Text = await GetCurrentLocation();  

        SemanticScreenReader.Announce(LocationTxt.Text);
	}

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    public async Task<string> GetCurrentLocation() {
        try {
            _isCheckingLocation = true;

            GeolocationRequest request = new(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
                return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex) {
            // Unable to get location
            return ex.Message;
        } finally {
            _isCheckingLocation = false;
        }
        return "None";
    }

    public void CancelRequest() {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
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

