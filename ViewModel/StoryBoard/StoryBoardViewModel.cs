using Magnify.Command;
using Magnify.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magnify.ViewModel
{
    public class StoryBoardViewModel : BaseViewModel
    {

        #region Data Full Properties
        public IList<string> ProjectNames { get; }

        private string _selectedProjectName;
        public string SelectedProjectName
        {
            get => _selectedProjectName;
            set
            {
                _selectedProjectName = value;
                SelectProjectAction();
                RaisePropertyChanged();
            }
        }

        private IList<WorkItemSingleViewModel> _workItems;
        public IList<WorkItemSingleViewModel> WorkItems
        {
            get => _workItems;
            set
            {
                _workItems = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command Get-Only Properties
        #endregion

        #region Constructor
        public StoryBoardViewModel()
        {
            ProjectNames = new List<string>();
        }
        #endregion

        #region Public Methods
        public void SelectProjectAction()
        {
            var projects = ProjectDataProvider.Instance.Projects;
            var selectedProject = projects.FirstOrDefault(project => project.Title == SelectedProjectName);

            var workItems = ProjectDataProvider.Instance.WorkItems;
            List<WorkItemSingleViewModel> workItemsForProject = new List<WorkItemSingleViewModel>();

            foreach(var workItem in workItems)
            {
                if(workItem.ProjectId == selectedProject.Id)
                {
                    workItemsForProject.Add(new WorkItemSingleViewModel(workItem, selectedProject));
                }
            }
            WorkItems = workItemsForProject;
        }
        public override async Task LoadAsync()
        {
            var projects = ProjectDataProvider.Instance.Projects;

            ProjectNames.Clear();

            foreach (var project in projects)
            {
                ProjectNames.Add(project.Title);
            }

            SelectedProjectName = ProjectNames[0];
        }
        #endregion

        #region Private Helpers
        #endregion


    }
}
