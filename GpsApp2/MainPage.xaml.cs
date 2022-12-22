using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GpsApp2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnLocate_Click(object sender, RoutedEventArgs e) {
            var locationAccessStatus = await Geolocator.RequestAccessAsync();
            if (locationAccessStatus == GeolocationAccessStatus.Allowed) {
                Geolocator geolocator = new Geolocator();
                var currentPosition = await geolocator.GetGeopositionAsync();
                var point = currentPosition.Coordinate.Point;
                mapControl.Center = point;
                mapControl.ZoomLevel = 16;

                txtLocation.Text = $"Latitude: {point.Position.Latitude}, Longitude: {point.Position.Longitude}";
            }
        }
    }
}
