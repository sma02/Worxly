using Worxly.Views;
using Avalonia.Media.TextFormatting.Unicode;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Refit;
using Worxly.Api;
using Worxly.DTOs;

namespace Worxly.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string email = string.Empty;
        private string password = string.Empty;
        public bool rememberMe = false;
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public string Email
        {
            get => email;
            set => this.RaiseAndSetIfChanged(ref email, value);
        }
        public string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }
        public LoginViewModel()
        {
            LoginCommand = ReactiveCommand.Create(LoginButtonClick);
            SignUpCommand = ReactiveCommand.Create(SignUpButtonClick);
        }
        public async void SignUpButtonClick()
        {
           Globals.Instance.Router.Navigate.Execute(new SignUpViewModel());
        }
        public async void LoginButtonClick()
        {
            Debug.WriteLine(Properties.Resources.DefaultHost);
            try
            {
                var userApi = RestService.For<IUserApi>(Properties.Resources.DefaultHost);
                var res = await userApi.Authenticate(Email, Password);
            if (res.StatusCode != System.Net.HttpStatusCode.OK)
                return;
            if (Globals.Instance.CurrentUserAuth is null && res.Content != null)
            {
                Globals.Instance.CurrentUser = res.Content;
                Globals.Instance.CurrentUser.Password = password;
                Globals.Instance.CurrentUserAuth = res.Content;
            }
            var content = res.Content;
            if (content.UserTypeVal == "Admin")
            {

            }
            else if (content.UserTypeVal == "Worker")
            {
            }
            else if (content.UserTypeVal == "User")
            {
                Globals.Instance.Router.Navigate.Execute(new ProfileViewModel(Globals.Instance.CurrentUser));
            }
            }
            catch (Exception ex) 
            {
                ConfirmationDialog dialog = new ConfirmationDialog()
                { Message = ex.Message,
                    PositiveText="OK",
                    NegativeButtonVisibility=false};
                var res = (bool)await dialog.Show();
            }
        }
    }
}
