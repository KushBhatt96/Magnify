using Magnify.Interfaces.Services;
using Magnify.Model.Messages;
using Magnify.Services;

namespace Magnify.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IMessengerService _messenger;

        private int _projectsCount;

        public DashboardViewModel()
        {
            _messenger = MessengerService.Instance;

            _messenger.Subscribe<ProjectsUpdatedMessage>(this, UpdateProjectsCountAction);
        }

        public int ProjectsCount
        {
            get => _projectsCount;
            set
            {
                _projectsCount = value;
                RaisePropertyChanged();
            }
        }

        public void UpdateProjectsCountAction(object state)
        {
            ProjectsCount = ((ProjectsUpdatedMessage)state).ProjectsCount;
        }
    }
}
