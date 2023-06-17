using Magnify.Command;
using Magnify.Model;
using Magnify.Model.Stores;

namespace Magnify.ViewModel
{
    public class ProjectItemViewModel : BaseViewModel
    {
        private readonly Project _project;

        public ProjectItemViewModel(Project project)
        {
            _project = project;
        }

        public int Id => _project.Id;

        public string? Title
        {
            get => _project.Title;
            set
            {
                _project.Title = value;
                RaisePropertyChanged();
            }
        }

        public string? Description
        {
            get => _project.Description;
            set
            {
                _project.Description = value;
                RaisePropertyChanged();
            }
        }

        public string CreatedAt => _project.CreatedAt != null ? _project.CreatedAt : "";

        public ProjectType ProjectType
        {
            get => _project.ProjectType;
            set
            {
                _project.ProjectType = value;
                RaisePropertyChanged();
            }
        }

        public ProjectStatus ProjectStatus
        {
            get => _project.ProjectStatus;
            set
            {
                _project.ProjectStatus = value;
                RaisePropertyChanged();
            }
        }
    }
}