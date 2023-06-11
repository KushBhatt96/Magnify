using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Model.Messages
{
    public class ProjectsUpdatedMessage
    {
        public int ProjectsCount { get; }

        public ProjectsUpdatedMessage(int projectsCount)
        {
            ProjectsCount = projectsCount;
        }
    }
}
