

using Magnify.Command;
using Magnify.Model.Stores;
using System;
using System.Windows;

namespace Magnify.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly BaseViewModel _initialViewModel;

        private string? _userName;
        private string? _password;

        public DelegateCommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationStore, BaseViewModel initialViewModel)
        {
            LoginCommand = new DelegateCommand(ExecuteLogin);

            _navigationStore = navigationStore;

            _initialViewModel = initialViewModel;
        }

        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
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

        public void ExecuteLogin(object? parameter)
        {
            try
            {
                MessageBox.Show($"Logging in {UserName}...");

                _navigationStore.SelectedViewModel = _initialViewModel;

            }
            catch (Exception)
            {
                // Log message over here
                MessageBox.Show("Login failed. Please try again.");
            }
        }
    }
}
