using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Worxly.ViewModels;

namespace Worxly.Views;

public partial class WorkerListView : ReactiveUserControl<WorkerListViewModel>
{
    public WorkerListView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}