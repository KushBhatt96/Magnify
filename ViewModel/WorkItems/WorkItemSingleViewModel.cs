using Magnify.Command;
using Magnify.Data;
using Magnify.Interfaces.Services;
using Magnify.Model;
using Magnify.Services;
using System;

namespace Magnify.ViewModel
{
    public class WorkItemSingleViewModel : BaseViewModel
    {
        private const string BaseIconPath = "/Resources/Images";
        private const string BugIconPath = $"{BaseIconPath}/bug.png";
        private const string StoryIconPath = $"{BaseIconPath}/story.png";

        private readonly WorkItem _workItem;
        private readonly Project _selectedProject;
        private readonly IMessengerService _messenger;
        private readonly INavigationService _navigationService;

        public DelegateCommand SaveCommand { get; }

        public WorkItemSingleViewModel(WorkItem workItem, Project selectedProject)
        {
            _workItem = workItem;
            _selectedProject = selectedProject;
            _messenger = MessengerService.Instance;
            _navigationService = NavigationService.GetInstance();
            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        public void Save(object? obj)
        {
            CreatedAt = DateTime.Now.ToShortDateString();
            _selectedProject.WorkItems.Add(_workItem);
            ProjectDataProvider.Instance.WorkItems.Add(_workItem);
            _messenger.Send<WorkItemSingleViewModel>(this);
            _navigationService.NavigateBack();
        }

        public bool CanSave(object? obj)
        {
            return !string.IsNullOrEmpty(Title);
        }

        public Guid Id => _workItem.WorkItemId;

        public Guid ProjectId => _workItem.ProjectId;

        public string Title
        {
            get => _workItem.Title;
            set
            {
                _workItem.Title = value;
                SaveCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _workItem.Description;
            set
            {
                _workItem.Description = value;
                RaisePropertyChanged();
            }
        }

        public int StoryPoints
        {
            get => _workItem.StoryPoints;
            set
            {
                _workItem.StoryPoints = value;
                RaisePropertyChanged();
            }
        }

        public string CreatedAt
        {
            get
            {
                var parseSucceeded = DateTime.TryParse(_workItem.CreatedAt, out DateTime result);
                if (parseSucceeded)
                {
                    return result.ToLongDateString();
                }
                return string.Empty;
            }
            set
            {
                _workItem.CreatedAt = value;
                RaisePropertyChanged();
            }
        }

        public WorkItemType WorkItemType
        {
            get => _workItem.WorkItemType;
            set
            {
                _workItem.WorkItemType = value;
                RaisePropertyChanged();
            }
        }

        public WorkItemStatus WorkItemStatus
        {
            get => _workItem.WorkItemStatus;
            set
            {
                _workItem.WorkItemStatus = value;
                RaisePropertyChanged();
            }
        }

        public string WorkItemIconPath => _workItem.WorkItemType == WorkItemType.Bug ? BugIconPath : StoryIconPath;
    }
}
