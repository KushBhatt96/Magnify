using Magnify.Interfaces;
using Magnify.Model.Messages;
using Magnify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;

        private int _projectsCount;

        public DashboardViewModel(IMessenger messenger)
        {
            _messenger = messenger;

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
