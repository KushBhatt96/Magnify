using Magnify.Command;
using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.Model;
using Magnify.Services;
using System;

namespace Magnify.ViewModel
{
    public class ProjectItemViewModel : BaseViewModel
    {
        private readonly Project _project;
        private readonly IMessengerService _messenger;
        private readonly INavigationService _navigationService;

        public DelegateCommand SaveCommand { get; }

        public ProjectItemViewModel(Project project)
        {
            _project = project;
            _messenger = MessengerService.Instance;
            _navigationService = NavigationService.GetInstance();
            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        public void Save(object? obj)
        {
            CreatedAt = DateTime.Now.ToShortDateString();
            ProjectDataProvider.Instance.Projects.Add(_project);
            _messenger.Send<ProjectItemViewModel>(this);
            _navigationService.NavigateBack();
        }

        public bool CanSave(object? obj)
        {
            return !string.IsNullOrEmpty(Title);
        }

        public Guid Id => _project.Id;

        public string Title
        {
            get => _project.Title;
            set
            {
                _project.Title = value;
                SaveCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _project.Description;
            set
            {
                _project.Description = value;
                RaisePropertyChanged();
            }
        }

        public string? CreatedAt
        {
            get
            {
                bool parseSucceeded = DateTime.TryParse(_project.CreatedAt, out DateTime result);
                if (parseSucceeded)
                {
                    return result.ToLongDateString();
                }
                return string.Empty;
            }
            set
            {
                if(string.IsNullOrEmpty(_project.CreatedAt))
                {
                    _project.CreatedAt = DateTime.Now.ToShortDateString();
                    RaisePropertyChanged();
                }
            }
        }

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