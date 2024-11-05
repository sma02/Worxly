using ReactiveUI;
using System;

namespace Worxly.ViewModels;

public class ViewModelBase : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; set; } = Globals.Instance.CurrentMainViewModel;
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public bool EditMode { get; set; } = false;
}
