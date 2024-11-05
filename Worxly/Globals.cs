using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.ViewModels;

namespace Worxly
{
    public class Globals
    {
        private static readonly Lazy<Globals> _instance = new Lazy<Globals>(() => new Globals());
        public static Globals Instance => _instance.Value;
        private ViewModelBase _CurrentWindow;
        public ViewModelBase CurrentWindow
        {
            get { return _CurrentWindow; }
            set { CurrentMainViewModel.RaiseAndSetIfChanged(ref _CurrentWindow, value); }
        }
        public RoutingState Router { get; set; }
        public MainViewModel CurrentMainViewModel { get; set; }
        //public string DataDirectory { get; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Worxly/";
    }
}
