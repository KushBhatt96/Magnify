using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Model
{
    public class WorkItem
    {
        public Guid WorkItemId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }

        public int StoryPoints { get; set; }

        public WorkItemType WorkItemType { get; set; }

        public WorkItemStatus WorkItemStatus { get; set; }

        public Guid ProjectId { get; set; }

        public string CreatedAt { get; set; } = string.Empty;
    }

    public enum WorkItemType
    {
        Story,
        Bug
    }

    public enum WorkItemStatus
    {
        New,
        Development,
        Review,
        Testing,
        Resolved
    }
}
