using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magnify.Data;
using Magnify.Model;

namespace Magnify.ViewModel
{
    public class ProjectsViewModel : BaseViewModel
    {
        private readonly IProjectDataProvider _projectDataProvider;

        // The ObservableCollection<T> class raises a CollectionChangedEvent when we
        // add or remove projects from the collection. The data binding in WPF subscribes to
        // that event to update the UI accordingly.
        // By making this property get-only, we are making it a "readonly property"
        public ObservableCollection<ProjectItemViewModel> Projects { get; } = new ObservableCollection<ProjectItemViewModel>();

        public ProjectsViewModel(IProjectDataProvider projectDataProvider)
        {
            _projectDataProvider = projectDataProvider;
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
        }
    }
}
