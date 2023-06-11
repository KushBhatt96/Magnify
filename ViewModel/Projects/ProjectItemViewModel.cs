using Magnify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.ViewModel
{
    public class ProjectItemViewModel : BaseViewModel
    {
        #region fields
        private readonly Project _project;
        #endregion

        #region ctor
        public ProjectItemViewModel(Project project)
        {
            _project = project;
        }
        #endregion

        #region full properties
        // Make Id a readonly property by not having a setter. We can use expression-bodied syntax here.
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

        // Make CreatedAt a readonly property by not having a setter. We can use expression-bodied syntax here.
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
        #endregion
    }
}