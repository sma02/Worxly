using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Worxly.ViewModels;
using static System.Net.WebRequestMethods;

namespace Worxly.Views;

public partial class AddServiceView : ReactiveUserControl<AddServiceViewModel>
{
    public AddServiceView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
    private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image file",
            AllowMultiple = false,
            FileTypeFilter = [FilePickerFileTypes.ImagePng, FilePickerFileTypes.ImageJpg]
        });
        if (files.Count > 0)
        {
            string image = await Helpers.ImageHelper.UploadImage(files[0]);
            if (DataContext is not null)
                ((AddServiceViewModel)DataContext).CurrentImageFile = image;
        }
    }
}