using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using Worxly.ViewModels;

namespace Worxly.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}