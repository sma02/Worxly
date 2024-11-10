using ReactiveUI;
using System;

namespace Worxly.ViewModels;

public class MainViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    public MainViewModel()
    {
        Globals.Instance.CurrentMainViewModel = this;
        Globals.Instance.Router = Router;

        Router.Navigate.Execute(new AddServiceViewModel());
    }
}
