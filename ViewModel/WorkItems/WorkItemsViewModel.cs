using Magnify.Command;
using Magnify.Data;
using Magnify.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Magnify.ViewModel
{
    public class WorkItemsViewModel : BaseViewModel
    {
        
        public WorkItemsViewModel()
        {
            ProjectNames = new List<string>();
        }

        // Full Property = data

        public IList<string> ProjectNames { get; }

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
                _searchText = value;
                FilteredWorkItems = new ObservableCollection<WorkItemSingleViewModel>(WorkItems.Where(item => item.Title.ToLower().Contains(_searchText.ToLower())));
                RaisePropertyChanged();
            }
        }


        // Get only Delegate Command => action/behaviour

        private void SelectProjectAction()
        {
            var projects = ProjectDataProvider.Instance.Projects;
            var project = projects.FirstOrDefault(project => project.Title == _selectedProjectName);
            ObservableCollection<WorkItemSingleViewModel> newWorkItems = new ObservableCollection<WorkItemSingleViewModel>();
            foreach(WorkItem item in project.WorkItems)
            {
                newWorkItems.Add(new WorkItemSingleViewModel(item));
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
                FilteredWorkItems = _workItems;
                RaisePropertyChanged();
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
            if (WorkItems.Any())
            {
                return;
            }

            WorkItems.CollectionChanged += OnCollectionChanged;

            var workItems = ProjectDataProvider.Instance.WorkItems;
            var projects = ProjectDataProvider.Instance.Projects;

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
    }
}
