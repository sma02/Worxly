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
        public void SignUpButtonClick()
        {

        }
        public void LoginButtonClick()
        {

        }
    }
}
