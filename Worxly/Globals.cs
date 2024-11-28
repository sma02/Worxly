using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs;
using Worxly.Helpers;
using Worxly.ViewModels;

namespace Worxly
{
    public class Globals
    {
        public event EventHandler? UserStatusChanged;

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
        public User user;
        public User CurrentUser { get=>user; set
            {
                user = value;
                AppConfig.Instance.User = value;
                AppConfig.SaveConfig();
                UserStatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public IUserAuth CurrentUserAuth { get; set; }
        public string DataDirectory { get; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Worxly/";
   
        private Globals()
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }
    }
}
