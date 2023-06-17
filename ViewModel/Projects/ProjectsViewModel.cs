using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IProjectDataProvider _projectDataProvider;

        private readonly IMessengerService _messenger;

        private readonly INavigationService _navigationService;

        private ProjectItemViewModel? _selectedProject;
        #endregion

        #region Public Properties
        public DelegateCommand AddProjectCommand { get; }

        public DelegateCommand DeleteProjectCommand { get; }

        public DelegateCommand NavigateToProjecItemCommand { get; }

        public ObservableCollection<ProjectItemViewModel> Projects { get; } = new ObservableCollection<ProjectItemViewModel>();
        #endregion

        public ProjectsViewModel(IProjectDataProvider projectDataProvider)
        {
            _projectDataProvider = projectDataProvider;
            _messenger = MessengerService.Instance;
            _navigationService = NavigationService.Instance;
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
        #endregion

        #region Public Methods
        public void AddProject(object? parameter)
        {
            Project project = new Project
            {
                Id = Projects.Count + 1,
                Title = "NEW PROJECT",
                Description = "This is a cool new one.",
                CreatedAt = DateTime.Now.ToShortDateString(),
                ProjectType = ProjectType.Mobile,
                ProjectStatus = ProjectStatus.New
            };

            ProjectItemViewModel projectItem = new ProjectItemViewModel(project);

            Projects.Add(projectItem);
            _messenger.Send(new ProjectsUpdatedMessage(Projects.Count));
        }

        public void DeleteProject(object? parameter)
        {
            if(SelectedProject == null)
            {
                return;
            }

            Projects.Remove(SelectedProject);
            _messenger.Send(new ProjectsUpdatedMessage(Projects.Count));
        }

        public void NavigateToProjectItem(object? parameter)
        {
            if(SelectedProject == null)
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

            var projects = await _projectDataProvider.GetProjectsAsync();

            if (projects != null)
            {
                foreach (var project in projects)
                {
                    Projects.Add(new ProjectItemViewModel(project));
                }
            }
            _messenger.Send(new ProjectsUpdatedMessage(Projects.Count));
        }
        #endregion
    }
}
