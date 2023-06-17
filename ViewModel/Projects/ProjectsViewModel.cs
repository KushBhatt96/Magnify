using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Magnify.Command;
using Magnify.Data;
using Magnify.Interfaces;
using Magnify.Model;
using Magnify.Model.Messages;
using Magnify.Model.Stores;

namespace Magnify.ViewModel
{
    public class ProjectsViewModel : BaseViewModel
    {
        #region Private Fields
        private readonly IProjectDataProvider _projectDataProvider;

        private readonly IMessenger _messenger;

        private readonly NavigationStore _navigationStore;

        private ProjectItemViewModel? _selectedProject;
        #endregion

        #region Public Properties
        public DelegateCommand AddProjectCommand { get; }

        public DelegateCommand DeleteProjectCommand { get; }

        public DelegateCommand NavigateToProjectDetailsCommand { get; }

        public ObservableCollection<ProjectItemViewModel> Projects { get; } = new ObservableCollection<ProjectItemViewModel>();
        #endregion

        public ProjectsViewModel(IProjectDataProvider projectDataProvider, IMessenger messenger, NavigationStore navigationStore)
        {
            _projectDataProvider = projectDataProvider;
            _messenger = messenger;
            _navigationStore = navigationStore;
            AddProjectCommand = new DelegateCommand(AddProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProject);
            NavigateToProjectDetailsCommand = new DelegateCommand(NavigateToProjectDetails);
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

        public void NavigateToProjectDetails(object? parameter)
        {
            if(SelectedProject == null)
            {
                return;
            }

            _navigationStore.SelectedViewModel = SelectedProject;
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
