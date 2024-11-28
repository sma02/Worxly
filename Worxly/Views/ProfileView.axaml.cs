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
        var mapControl = this.FindControl<MapControl>("mapControl");
        ProfileViewModel? profileViewModel = (ProfileViewModel?)DataContext;
        if (profileViewModel != null && mapControl != null)
            mapControl.Map = profileViewModel.MapViewModel.Map;
    }
}