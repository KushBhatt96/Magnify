using Magnify.Command;
using Magnify.Interfaces.Services;
using Magnify.Services;
using System;
using System.Windows;

namespace Magnify.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authService;
        
        private string? _username;
        private string? _password;

        public DelegateCommand LoginCommand { get; }

        public LoginViewModel()
        {
            _authService = AuthenticationService.Instance;

            LoginCommand = new DelegateCommand(Login);
        }

        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged();
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public void Login(object? parameter)
        {
            Password = "Test";
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please fill in your credentials.");
                return;
            }
            try
            {
                _authService.Login(Username, Password);
            }
            catch (Exception)
            {
                // TODO: Add logging here
                MessageBox.Show("Login failed. Please try again.");
            }
        }
    }
}
