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
