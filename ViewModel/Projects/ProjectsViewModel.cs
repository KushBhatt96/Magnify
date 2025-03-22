using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Magnify.Command;
using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.Model;
using Magnify.Model.Messages;
using Magnify.Services;

namespace Magnify.ViewModel
{
    public class ProjectsViewModel : BaseViewModel
    {
        #region Private Fields
        private readonly IMessengerService _messenger;

        private readonly INavigationService _navigationService;

        private ProjectItemViewModel? _selectedProject;

        private string _searchText;
        #endregion

        #region Public Properties
        public DelegateCommand AddProjectCommand { get; }

        public DelegateCommand DeleteProjectCommand { get; }

        public DelegateCommand NavigateToProjecItemCommand { get; }

        private ObservableCollection<ProjectItemViewModel> Projects { get; } = new ObservableCollection<ProjectItemViewModel>();

        private ObservableCollection<ProjectItemViewModel> _filteredProjects = new ObservableCollection<ProjectItemViewModel>();
        public ObservableCollection<ProjectItemViewModel> FilteredProjects
        {
            get => _filteredProjects;
            set
            {
                _filteredProjects = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ProjectsViewModel(ProjectDataProvider projectDataProvider)
        {
            _messenger = MessengerService.Instance;
            _messenger.Subscribe<ProjectItemViewModel>(this, UpdateProjectsAction);
            _navigationService = NavigationService.GetInstance();
            AddProjectCommand = new DelegateCommand(AddProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProject);
            NavigateToProjecItemCommand = new DelegateCommand(NavigateToProjectItem);
        }

        #region Full Properties
        public ProjectItemViewModel? SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                RaisePropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (value != null)
                {
                    _searchText = value;
                    FilteredProjects = new ObservableCollection<ProjectItemViewModel>(Projects.Where(p => p.Title.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase)));
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region Public Methods
        public void AddProject(object? parameter)
        {
            Guid newId = Guid.NewGuid();
            Project project = new Project()
            {
                Id = newId
            };
            ProjectItemViewModel projectItem = new ProjectItemViewModel(project);

            _navigationService.Navigate(projectItem);
            SearchText = string.Empty;
        }

        public void DeleteProject(object? parameter)
        {
            if (SelectedProject == null)
            {
                return;
            }

            var workItems = ProjectDataProvider.Instance.WorkItems;
            foreach(var workItem in workItems.ToList())
            {
                if(workItem.ProjectId == SelectedProject.Id)
                {
                    workItems.Remove(workItem);
                }
            }

            var projects = ProjectDataProvider.Instance.Projects;
            foreach(var project in projects.ToList())
            {
                if(project.Id == SelectedProject.Id)
                {
                    projects.Remove(project);
                }
            }
            
            Projects.Remove(SelectedProject);
            SearchText = string.Empty;
        }

        public void NavigateToProjectItem(object? parameter)
        {
            if (SelectedProject == null)
            {
                return;
            }
            _navigationService.Navigate(SelectedProject);
        }

        public override async Task LoadAsync()
        {
            if (Projects.Any())
            {
                return;
            }

            Projects.CollectionChanged += OnCollectionChanged;

            var projects = ProjectDataProvider.Instance.Projects;

            if (projects != null)
            {
                foreach (var project in projects)
                {
                    Projects.Add(new ProjectItemViewModel(project));
                }
            }
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            FilteredProjects = Projects;
            _messenger.Send(new ProjectsUpdatedMessage(Projects.Count));
        }

        private void UpdateProjectsAction(object state)
        {
            try
            {
                ProjectItemViewModel project = (ProjectItemViewModel)state;
                if (Projects.SingleOrDefault(p => p.Id == project.Id) == null)
                {
                    Projects.Add(project);
                }
            }
            catch (InvalidCastException)
            {
                // TODO: Logging here
                MessageBox.Show("Error: The project state is not valid.");
            }
            catch (Exception)
            {
                // TODO: Logging here
                MessageBox.Show("Error: Unknown error occurred while adding project.");
            }
        }
        #endregion
    }
}
