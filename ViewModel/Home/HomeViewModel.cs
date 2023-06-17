using Magnify.Command;
using Magnify.Model.Stores;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Magnify.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

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

        public HomeViewModel(DashboardViewModel dashboardViewModel, ProjectsViewModel projectsViewModel, WorkItemsViewModel workItemsViewModel, NavigationStore navigationStore)
        {
            DashboardViewModel = dashboardViewModel;
            ProjectsViewModel = projectsViewModel;
            WorkItemsViewModel = workItemsViewModel;

            NavigateBackCommand = new DelegateCommand(NavigateBackward, CanNavigateBackward);
            NavigateForwardCommand = new DelegateCommand(NavigateForward, CanNavigateForward);

            SelectedMainFrameViewModel = projectsViewModel;

            SelectMainFrameViewModelCommand = new DelegateCommand(SelectMainFrameViewModel);

            _navigationStore = navigationStore;
            _navigationStore.NavigationChanged += HomeView_NavigationChanged;
        }

        public void SelectMainFrameViewModel(object? parameter)
        {
            try
            {
                var selectedViewModel = parameter as BaseViewModel;

                if (selectedViewModel != null)
                {
                    _navigationStore.SelectedViewModel = selectedViewModel;
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
            _navigationStore.PreviousViewModel();
        }

        public bool CanNavigateBackward(object? parameter) => _navigationStore.FirstStackCount() > 1;

        public void NavigateForward(object? parameter)
        {
            _navigationStore.NextViewModel();
        }

        public bool CanNavigateForward(object? parameter) => _navigationStore.SecondStackCount() > 0;

        public void HomeView_NavigationChanged()
        {
            SelectedMainFrameViewModel = _navigationStore.SelectedViewModel;
        }

        public async override Task LoadAsync()
        {
            if(SelectedMainFrameViewModel is not null)
            {
                await SelectedMainFrameViewModel.LoadAsync();
            }
        }

    }
}
