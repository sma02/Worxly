using ReactiveUI;
using System;

namespace Worxly.ViewModels;

public class MainViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    public MainViewModel()
    {
        Globals.CurrentMainViewModel = this;
        Globals.Router = Router;

        Router.Navigate.Execute(new SignUpViewModel());
    }
}
