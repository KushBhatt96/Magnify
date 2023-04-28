using Magnify.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.ViewModel
{
    public class MainViewModel : BaseViewModel
    {


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


        public DelegateCommand SelectViewModelCommand { get; }

        public DashboardViewModel DashboardViewModel { get; }
        public ProjectsViewModel ProjectsViewModel { get; }

        public WorkItemsViewModel WorkItemsViewModel { get; }

        public MainViewModel(DashboardViewModel dashboardViewModel, ProjectsViewModel projectsViewModel, WorkItemsViewModel workItemsViewModel)
        {
            DashboardViewModel = dashboardViewModel;
            ProjectsViewModel = projectsViewModel;
            WorkItemsViewModel = workItemsViewModel;
            SelectedViewModel = DashboardViewModel;

            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        public async void SelectViewModel(object? parameter)
        {
            try
            {
                SelectedViewModel = parameter as BaseViewModel;
                await LoadAsync();
            }
            catch(Exception ex)
            {
                // Add logging here
                // Show some error box to user
                Console.WriteLine($"{nameof(SelectViewModel)} - An error occurred while selecting the View.");
            }
        }

        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
    }
}
