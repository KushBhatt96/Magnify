using Magnify.Model;
using System;

namespace Magnify.ViewModel
{
    public class WorkItemSingleViewModel : BaseViewModel
    {
        private readonly WorkItem _workItem;
        public WorkItemSingleViewModel(WorkItem workItem)
        {
            _workItem = workItem;
        }

        public string Title
        {
            get => _workItem.Title;
            set
            {
                _workItem.Title = value;
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
    }
}
