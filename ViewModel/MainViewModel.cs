using Magnify.Interfaces.Services;
using Magnify.Model.Stores;
using Magnify.Services;
using System;
using System.Windows.Navigation;

namespace Magnify.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authService;

        private BaseViewModel? _selectedViewModel;

        public BaseViewModel? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            SelectedViewModel = new LoginViewModel();

            _authService = AuthenticationService.Instance;
            _authService.RedirectionOccurred += User_RedirectAfterLogin;
        }


        public void User_RedirectAfterLogin(BaseViewModel viewModel)
        {
            SelectedViewModel = viewModel;
            SelectedViewModel?.LoadAsync();
        }
    }
}
