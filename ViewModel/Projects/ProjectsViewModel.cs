using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Magnify.Command;
using Magnify.Data;
using Magnify.Interfaces;
using Magnify.Model;
using Magnify.Model.Messages;

namespace Magnify.ViewModel
{
    public class ProjectsViewModel : BaseViewModel
    {
        private readonly IProjectDataProvider _projectDataProvider;

        private readonly IMessenger _messenger;


        private ProjectItemViewModel? _selectedProject;

        public DelegateCommand AddProjectCommand { get; }

        public DelegateCommand DeleteProjectCommand { get; }

        public ObservableCollection<ProjectItemViewModel> Projects { get; } = new ObservableCollection<ProjectItemViewModel>();

        public ProjectsViewModel(IProjectDataProvider projectDataProvider, IMessenger messenger)
        {
            _projectDataProvider = projectDataProvider;
            _messenger = messenger;
            AddProjectCommand = new DelegateCommand(AddProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProject);
        }

        public ProjectItemViewModel? SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                RaisePropertyChanged();
            }
        }

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
    }
}
