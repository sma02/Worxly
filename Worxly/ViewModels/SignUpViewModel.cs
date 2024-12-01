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
using Worxly.Api;
using Worxly.DTOs;
using Refit;

namespace Worxly.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private string username = string.Empty;
        private string email = string.Empty;
        private string password = string.Empty;
        private bool isCustomer = true;

        public ICommand SignUpCommand { get; }
        public string FirstName
        {
            get => firstName;
            set => this.RaiseAndSetIfChanged(ref firstName, value);
        }
        public string LastName
        {
            get => lastName;
            set => this.RaiseAndSetIfChanged(ref lastName, value);
        }
        public string Username
        {
            get => username;
            set => this.RaiseAndSetIfChanged(ref username, value);
        }
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
        public bool IsCustomer
        {
            get => isCustomer;
            set => this.RaiseAndSetIfChanged(ref isCustomer, value);
        }
        public SignUpViewModel()
        {
            SignUpCommand = ReactiveCommand.Create(SignUpButtonClick);
        }
        public void SignUpButtonClick()
        {
            var userApi = RestService.For<IUserApi>(Properties.Resources.DefaultHost);
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Email = Email,
                Password = Password,
                UserTypeVal = IsCustomer ? "User" : "Worker"
            };
            var createdUser = userApi.PostUserAccount(user).Result;
            Globals.Instance.Router.Navigate.Execute(new LoginViewModel());
        }
    }
}
