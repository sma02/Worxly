using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
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
        // Get the top-level window
        var topLevel = TopLevel.GetTopLevel(this);

        if (topLevel != null)
        {
            // Start async operation to open the file dialog
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select a Picture",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                        FilePickerFileTypes.ImageAll
                    }
            });

            if (files.Count >= 1)
            {
                // Get the first selected file and open it for reading
                var selectedFile = files[0];
                await using var stream = await selectedFile.OpenReadAsync();

                // Optionally, you can process the file here, e.g., display it or store it
                using var reader = new StreamReader(stream);
                var fileContent = await reader.ReadToEndAsync();
            }
        }
    }
}