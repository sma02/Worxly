using Android.App;
using Android.Content.PM;
using Android.OS;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System.Threading;
using System;
using System.Threading.Tasks;
using Worxly.DeviceApi;

namespace Worxly.Android;
public class GeolocationApi : IGeolocationApi
{
    bool _isCheckingLocation;
    CancellationTokenSource _cancelTokenSource;
    public async Task<(double?, double?)> GetLocation()
    {
        try
        {
            // Keep these like this or it'll not work
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            Location? location = await Geolocation.GetLocationAsync(request);
            if (location != null)
                return (location.Latitude, location.Longitude);
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }
        return (null, null);
    }

}


[Activity(
    Label = "Worxly.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
            Globals.Instance.GeolocationApi = new GeolocationApi();
            return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);
    }
}
