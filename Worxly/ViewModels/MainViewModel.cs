using ReactiveUI;
using System;
using System.Reactive;
using System.Windows.Input;

namespace Worxly.ViewModels;

public class MainViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, IRoutableViewModel> DashboardButtonCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> ServiceViewButtonCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> ProfileButtonCommand { get; }

    private bool isUserLoggedIn = true;
    public bool IsUserLoggedIn
    {
        get => isUserLoggedIn;
        set
        {
            if (value == true)
                Globals.Instance.Router.Navigate.Execute(new UserViewModel());
            else if (value == false)
                Globals.Instance.Router.Navigate.Execute(new LoginViewModel());
            this.RaiseAndSetIfChanged(ref isUserLoggedIn, value);
        }
    }

    public MainViewModel()
    {
        Globals.Instance.CurrentMainViewModel = this;
        Globals.Instance.Router = Router;
        DashboardButtonCommand = ReactiveCommand.CreateFromObservable(DashboardButtonClick);
        ServiceViewButtonCommand = ReactiveCommand.CreateFromObservable(ServiceViewButtonClick);
        ProfileButtonCommand = ReactiveCommand.CreateFromObservable(ProfileButtonClick);

        Globals.Instance.UserStatusChanged += (s, e) => IsUserLoggedIn = Globals.Instance.CurrentUser != null;
        IsUserLoggedIn = Globals.Instance.CurrentUser != null;
    }
    // navigation will be added when views will create
    public IObservable<IRoutableViewModel> DashboardButtonClick() => Globals.Instance.Router.Navigate.Execute(new DashboardViewModel(Globals.Instance.CurrentUser));
    public IObservable<IRoutableViewModel> ServiceViewButtonClick() => Globals.Instance.Router.Navigate.Execute(new UserViewModel());
    public IObservable<IRoutableViewModel> ProfileButtonClick() => Globals.Instance.Router.Navigate.Execute(new ProfileViewModel(Globals.Instance.CurrentUser));

}
