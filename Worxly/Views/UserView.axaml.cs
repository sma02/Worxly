using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Diagnostics;
using Worxly.DTOs;
using Worxly.ViewModels;

namespace Worxly.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }

    private void RefreshContainer_RefreshRequested(object? sender, Avalonia.Controls.RefreshRequestedEventArgs e)
    {
        var deferral = e.GetDeferral();

        deferral.Complete();
    }
}