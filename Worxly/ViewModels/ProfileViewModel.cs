using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private User user;
        public User User
        {
            get => user;
            set => this.RaiseAndSetIfChanged(ref user, value);
        }
        private MapViewModel mapViewModel;
        public MapViewModel MapViewModel
        {
            get => mapViewModel;
            set => this.RaiseAndSetIfChanged(ref mapViewModel, value);
        }

        public string FullName => $"Wali Ahmad";
        public string ProfileImageUrl => "avares://Worxly/Assets/avalonia-logo.ico";

        public ProfileViewModel(User user)
        {
            User = user;
            MapViewModel = new MapViewModel(69, 30);
        }
    }
}
