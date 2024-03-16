using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        public IList<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }

    public enum ProjectType
    {
        Desktop,
        WebClient,
        WebServer,
        Mobile,
        Game,
        Other
    }

    public enum ProjectStatus
    {
        New,
        Active,
        OnHold,
        Completed,
        Closed
    }
}
