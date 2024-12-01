using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Mapsui.UI.Avalonia;
using ReactiveUI;
using System.Diagnostics;
using Worxly.ViewModels;

namespace Worxly.Views;

public partial class ProfileView : ReactiveUserControl<ProfileViewModel>
{
    public ProfileView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
        DataContextChanged += ProfileView_DataContextChanged;
    }

    private void ProfileView_DataContextChanged(object? sender, System.EventArgs e)
    {
        
        ProfileViewModel? profileViewModel = (ProfileViewModel?)DataContext;
        profileViewModel.LocationAcquired += (s, e) =>
        {
            var mapControl = this.FindControl<MapControl>("mapControl");
            if (profileViewModel != null && mapControl != null)
                mapControl.Map = profileViewModel.MapHelper.Map;
        };
    }
}