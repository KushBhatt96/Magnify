using Magnify.Command;
using Magnify.Interfaces.Services;
using Magnify.Services;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Magnify.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authService;

        private BaseViewModel? _selectedMainFrameViewModel;

        public BaseViewModel? SelectedMainFrameViewModel
        {
            get => _selectedMainFrameViewModel;
            set
            {
                _selectedMainFrameViewModel = value;

                NavigateBackCommand.RaiseCanExecuteChanged();
                NavigateForwardCommand.RaiseCanExecuteChanged();

                RaisePropertyChanged();
            }
        }

        public DashboardViewModel DashboardViewModel { get; }
        public ProjectsViewModel ProjectsViewModel { get; }
        public WorkItemsViewModel WorkItemsViewModel { get; }

        public DelegateCommand SelectMainFrameViewModelCommand { get; }
        public DelegateCommand NavigateBackCommand { get; }
        public DelegateCommand NavigateForwardCommand { get; }

        public DelegateCommand LogoutCommand { get; }

        public HomeViewModel(DashboardViewModel dashboardViewModel, ProjectsViewModel projectsViewModel, WorkItemsViewModel workItemsViewModel)
        {
            DashboardViewModel = dashboardViewModel;
            ProjectsViewModel = projectsViewModel;
            WorkItemsViewModel = workItemsViewModel;

            NavigateBackCommand = new DelegateCommand(NavigateBackward
                //CanNavigateBackward
                );
            NavigateForwardCommand = new DelegateCommand(NavigateForward
                //CanNavigateForward
                );

            LogoutCommand = new DelegateCommand(Logout);

            SelectedMainFrameViewModel = projectsViewModel;

            SelectMainFrameViewModelCommand = new DelegateCommand(SelectMainFrameViewModel);

            _navigationService = NavigationService.Instance;
            _navigationService.NavigationChanged += HomeView_NavigationChanged;

            _authService = AuthenticationService.Instance;
        }

        public void SelectMainFrameViewModel(object? parameter)
        {
            try
            {
                var selectedViewModel = parameter as BaseViewModel;

                if (selectedViewModel != null)
                {
                    _navigationService.Navigate(selectedViewModel);
                }

            }
            catch (Exception)
            {
                // TODO: Add Logging here
                MessageBox.Show("Error occurred while changing main frame view.");
            }
        }

        public void NavigateBackward(object? parameter)
        {
            //_navigationStore.PreviousViewModel();
        }

        //public bool CanNavigateBackward(object? parameter) => _navigationStore.FirstStackCount() > 1;

        public void NavigateForward(object? parameter)
        {
            //_navigationStore.NextViewModel();
        }

        //public bool CanNavigateForward(object? parameter) => _navigationStore.SecondStackCount() > 0;

        public void Logout(object? parameter)
        {
            _authService.Logout();
        }

        public void HomeView_NavigationChanged(object? sender, EventArgs e)
        {
            SelectedMainFrameViewModel = _navigationService.CurrentNavigationState();
        }

        public async override Task LoadAsync()
        {
            if (SelectedMainFrameViewModel is not null)
            {
                await SelectedMainFrameViewModel.LoadAsync();
            }
        }

    }
}
