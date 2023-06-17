using Magnify.Command;
using Magnify.Model.Stores;
using System;
using System.Windows;

namespace Magnify.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly BaseViewModel _redirectionViewModel;
        private readonly NavigationStore _navigationStore;
        
        private string? _username;
        private string? _password;

        public DelegateCommand LoginCommand { get; }

        public LoginViewModel(BaseViewModel redirectionViewModel, NavigationStore navigationStore)
        {
            _redirectionViewModel = redirectionViewModel;
            _navigationStore = navigationStore;

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
            try
            {
                MessageBox.Show($"Logging in {Username}...");
                _navigationStore.SelectedViewModel = _redirectionViewModel;
            }
            catch (Exception)
            {
                // TODO: Add logging here
                MessageBox.Show("Login failed. Please try again.");
            }
        }
    }
}
