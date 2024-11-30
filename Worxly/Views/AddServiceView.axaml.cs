using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Collections.Generic;
using System.IO;
using Worxly.ViewModels;

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
        var dialog = new OpenFileDialog
        {
            Title = "Select an Image",
            Filters = new List<FileDialogFilter> { new FileDialogFilter { Name = "Image Files", Extensions = { "jpg", "png", "jpeg" } } }
        };

        var result = await dialog.ShowAsync((Window)this.VisualRoot);

        if (result != null && result.Length > 0)
        {
            string filePath = result[0];

            // Convert image to base64
            string base64Image = Worxly.Helpers.ImageHelper.ConvertImageToBase64(filePath);

            // Store in ViewModel
            //((AddServiceViewModel)this.DataContext).Imageurl = base64Image;
        }
    }
}