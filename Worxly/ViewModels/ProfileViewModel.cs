using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Worxly.DTOs;
using Worxly.Helpers;

namespace Worxly.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public EventHandler LocationAcquired;
        public ICommand EditProfileCommand { get; }
        private User user;
        public User User
        {
            get => user;
            set => this.RaiseAndSetIfChanged(ref user, value);
        }
        private MapHelper mapHelper = new MapHelper();
        public MapHelper MapHelper
        {
            get => mapHelper;
        }

        public string FullName => $"Wali Ahmad";
        public string ProfileImageUrl => "avares://Worxly/Assets/avalonia-logo.ico";

        public ProfileViewModel(User user)
        {
            User = user;
            FetchLocation();
        }
        public async void FetchLocation()
        {
            try
            {
                if (Globals.Instance.GeolocationApi is null)
                    throw new NotSupportedException("Geolocation API is not supported on this platform.");
                (double?, double?) val = await Globals.Instance.GeolocationApi.GetLocation();
                await MapHelper.InitializeMapAsync((double)val.Item1, (double)val.Item2);
                LocationAcquired?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
