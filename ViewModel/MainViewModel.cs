using Magnify.Model.Stores;

namespace Magnify.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

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

        public LoginViewModel LoginViewModel { get; }
        public HomeViewModel HomeViewModel { get; }

        public MainViewModel(LoginViewModel loginViewModel, HomeViewModel homeViewModel, NavigationStore navigationStore)
        {
            LoginViewModel = loginViewModel;
            HomeViewModel = homeViewModel;

            SelectedViewModel = loginViewModel;

            _navigationStore = navigationStore;
            _navigationStore.NavigationChanged += User_NavigateAfterLogin;
        }


        public void User_NavigateAfterLogin()
        {
            SelectedViewModel = _navigationStore.SelectedViewModel;
            SelectedViewModel.LoadAsync();
        }
    }
}
