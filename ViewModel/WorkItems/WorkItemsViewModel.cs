using Magnify.Command;
using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.Model;
using Magnify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Magnify.ViewModel
{
    public class WorkItemsViewModel : BaseViewModel
    {
        private readonly IMessengerService _messengerService;
        private readonly INavigationService _navigationService;
        public WorkItemsViewModel()
        {
            ProjectNames = new List<string>();
            DeleteWorkItemCommand = new DelegateCommand(DeleteWorkItem);
            AddWorkItemCommand = new DelegateCommand(AddWorkItem);
            NavigateToSingleWorkItemCommand = new DelegateCommand(NavigateToSingleWorkItem);
            _navigationService = NavigationService.GetInstance();
            _messengerService = MessengerService.Instance;
            _messengerService.Subscribe<WorkItemSingleViewModel>(this, UpdateWorkItemsAction);
        }

        // Full Property = data

        public IList<string> ProjectNames { get; private set; }

        private string _selectedProjectName;
        public string SelectedProjectName
        {
            get => _selectedProjectName;
            set
            {
                _selectedProjectName = value;
                SelectProjectAction();
                RaisePropertyChanged();
            }
        }
        private WorkItemSingleViewModel _selectedWorkItem;
        public WorkItemSingleViewModel SelectedWorkItem
        {
            get => _selectedWorkItem;
            set
            {
                _selectedWorkItem = value;
                RaisePropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (value != null)
                {
                    _searchText = value;
                    FilteredWorkItems = new ObservableCollection<WorkItemSingleViewModel>(WorkItems.Where(item => item.Title.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase)));
                    RaisePropertyChanged();
                }
            }
        }


        // Get only Delegate Command => action/behaviour
        public DelegateCommand DeleteWorkItemCommand { get; }
        public DelegateCommand AddWorkItemCommand { get; }

        public DelegateCommand NavigateToSingleWorkItemCommand { get; }

        public void DeleteWorkItem(object? obj)
        {
            var workItems = ProjectDataProvider.Instance.WorkItems;
            var workItemToDelete = workItems.FirstOrDefault(workItem => workItem.WorkItemId == SelectedWorkItem.Id);

            var project = ProjectDataProvider.Instance.Projects.FirstOrDefault(project => project.Id == SelectedWorkItem.ProjectId);
            
            if(workItemToDelete != null)
            {
                workItems.Remove(workItemToDelete);
                project.WorkItems.Remove(workItemToDelete);
            }

            WorkItems.Remove(SelectedWorkItem);
            SearchText = string.Empty;
        }

        public void AddWorkItem(object? obj)
        {
            Guid id = Guid.NewGuid();
            var selectedProject = ProjectDataProvider.Instance.Projects.FirstOrDefault(project => project.Title == SelectedProjectName);

            if (selectedProject == null) return;
            
            var newWorkItem = new WorkItem
            {
                WorkItemId = id,
                ProjectId = selectedProject.Id
            };

            var newWorkItemSingleViewModel = new WorkItemSingleViewModel(newWorkItem, selectedProject);

            _navigationService.Navigate(newWorkItemSingleViewModel);
            SearchText = string.Empty;
        }

        public void NavigateToSingleWorkItem(object? param)
        {
            if(SelectedWorkItem == null)
            {
                return;
            }
            _navigationService.Navigate(SelectedWorkItem);
        }

        private void SelectProjectAction()
        {
            var projects = ProjectDataProvider.Instance.Projects;
            var project = projects.FirstOrDefault(project => project.Title == _selectedProjectName);
            ObservableCollection<WorkItemSingleViewModel> newWorkItems = new ObservableCollection<WorkItemSingleViewModel>();
            foreach(WorkItem item in project.WorkItems)
            {
                newWorkItems.Add(new WorkItemSingleViewModel(item, project));
            }

            WorkItems = newWorkItems;
        }

        private ObservableCollection<WorkItemSingleViewModel> _workItems = new ObservableCollection<WorkItemSingleViewModel>();
        public ObservableCollection<WorkItemSingleViewModel> WorkItems
        {
            get => _workItems;
            set
            {
                _workItems = value;
                FilteredWorkItems = value;
            }
        }

        private ObservableCollection<WorkItemSingleViewModel> _filterdWorkItems = new ObservableCollection<WorkItemSingleViewModel>();
        public ObservableCollection<WorkItemSingleViewModel> FilteredWorkItems
        {
            get => _filterdWorkItems;
            set
            {
                _filterdWorkItems = value;
                RaisePropertyChanged();
            }
        }

        

        public override async Task LoadAsync()
        {
            WorkItems.CollectionChanged += OnCollectionChanged;

            var workItems = ProjectDataProvider.Instance.WorkItems;
            var projects = ProjectDataProvider.Instance.Projects;

            ProjectNames.Clear();

            if(projects != null)
            {
                foreach(Project project in projects)
                {
                    ProjectNames.Add(project.Title);
                }
            }

            SelectedProjectName = ProjectNames[0];
            // TODO: Send message to Dashboard to update itself with WorkItem data
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            FilteredWorkItems = WorkItems;
        }

        private void UpdateWorkItemsAction(object state)
        {
            try
            {
                WorkItemSingleViewModel workItem = (WorkItemSingleViewModel)state;
                if (WorkItems.SingleOrDefault(w => w.Id == workItem.Id) == null)
                {
                    WorkItems.Add(workItem);
                    FilteredWorkItems = WorkItems;
                }
            }
            catch(InvalidCastException)
            {
                // TODO: Logging here
                MessageBox.Show("Error: The WorkItem state is not valid.");
            }
            catch (Exception)
            {
                // TODO: Logging here
                MessageBox.Show("Error: Unknown error occurred while adding WorkItem.");
            }

        }
    }
}
