﻿using Magnify.Command;
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

                RaisePropertyChanged();
            }
        }

        // Below are all of the ViewModel options that SelectedMainFrameViewModel can be set to, they are initialized in the ctor
        public DashboardViewModel DashboardViewModel { get; }
        public ProjectsViewModel ProjectsViewModel { get; }
        public WorkItemsViewModel WorkItemsViewModel { get; }
        public StoryBoardViewModel StoryBoardViewModel { get; }
        public ChatViewModel ChatViewModel { get; }

        public AsyncDelegateCommand SelectMainFrameViewModelCommand { get; }
        public DelegateCommand NavigateBackCommand { get; }
        public DelegateCommand NavigateForwardCommand { get; }

        public DelegateCommand LogoutCommand { get; }

        public HomeViewModel(DashboardViewModel dashboardViewModel, ProjectsViewModel projectsViewModel, WorkItemsViewModel workItemsViewModel, 
                             StoryBoardViewModel storyBoardViewModel, ChatViewModel chatViewModel)
        {
            DashboardViewModel = dashboardViewModel;
            ProjectsViewModel = projectsViewModel;
            WorkItemsViewModel = workItemsViewModel;
            StoryBoardViewModel = storyBoardViewModel;
            ChatViewModel = chatViewModel;

            SelectedMainFrameViewModel = dashboardViewModel;
            _navigationService = NavigationService.GetInstance(SelectedMainFrameViewModel);

            LogoutCommand = new DelegateCommand(Logout);

            SelectMainFrameViewModelCommand = new AsyncDelegateCommand(SelectMainFrameViewModel);

            
            _navigationService.NavigationChanged += HomeView_NavigationChanged;

            NavigateBackCommand = new DelegateCommand(NavigateBackward, (object? obj) => _navigationService.CanNavigateBack());
            NavigateForwardCommand = new DelegateCommand(NavigateForward, (object? obj) => _navigationService.CanNavigateForward());

            _authService = AuthenticationService.Instance;
        }

        public async Task SelectMainFrameViewModel(object? parameter)
        {
            try
            {
                var selectedViewModel = parameter as BaseViewModel;

                if (selectedViewModel != null)
                {
                    await selectedViewModel.LoadAsync();
                    _navigationService.Navigate(selectedViewModel);
                    NavigateBackCommand.RaiseCanExecuteChanged();
                    NavigateForwardCommand.RaiseCanExecuteChanged();
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
            _navigationService.NavigateBack();
            NavigateBackCommand.RaiseCanExecuteChanged();
            NavigateForwardCommand.RaiseCanExecuteChanged();
        }

        //public bool CanNavigateBackward(object? parameter) => _navigationStore.FirstStackCount() > 1;

        public void NavigateForward(object? parameter)
        {
            _navigationService.NavigateForward();
            NavigateForwardCommand.RaiseCanExecuteChanged();
            NavigateBackCommand.RaiseCanExecuteChanged();
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
            // Load each of the views for the application
            // The reason we load the data for all views right away is so that the Dashboard can get latest metrics
            // and we can show it
            await DashboardViewModel.LoadAsync();
            await ProjectsViewModel.LoadAsync();
            await WorkItemsViewModel.LoadAsync();
            await StoryBoardViewModel.LoadAsync();
            await ChatViewModel.LoadAsync();
        }

    }
}
