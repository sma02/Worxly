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
        private static ViewModelBase _CurrentWindow;
        public static ViewModelBase CurrentWindow
        {
            get { return _CurrentWindow; }
            set { CurrentMainViewModel.RaiseAndSetIfChanged(ref _CurrentWindow, value); }
        }
        public static RoutingState Router { get; set; }
        public static MainViewModel CurrentMainViewModel { get; set; }
        //public static string DataDirectory { get; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Worxly/";
    }
}
