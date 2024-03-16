using Magnify.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magnify.Data
{

    public class ProjectDataProvider
    {
        private static ProjectDataProvider _instance;

        public IList<Project> Projects { get; set; }
        public IList<WorkItem> WorkItems { get; set; }

        private ProjectDataProvider()
        {
            GetProjects();
            GetWorkItems();
            CreateRelationShip();
        }

        public static ProjectDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProjectDataProvider();
                }
                return _instance;
            }
        }

        private void CreateRelationShip()
        {
            WorkItems[0].ProjectId = Projects[0].Id;
            WorkItems[1].ProjectId = Projects[0].Id;
            WorkItems[2].ProjectId = Projects[0].Id;
            WorkItems[3].ProjectId = Projects[2].Id;

            Projects[0].WorkItems.Add(WorkItems[0]);
            Projects[0].WorkItems.Add(WorkItems[1]);
            Projects[0].WorkItems.Add(WorkItems[2]);
            Projects[2].WorkItems.Add(WorkItems[3]);
        }
        private void GetWorkItems()
        {
            var workItemIdOne = Guid.NewGuid();
            var workItemIdTwo = Guid.NewGuid();
            var workItemIdThree = Guid.NewGuid();
            var workItemIdFour = Guid.NewGuid();

            WorkItems = new List<WorkItem>
                    {
                        new WorkItem
                        {
                            WorkItemId = workItemIdOne,
                            Title = "Complete the Authentication!",
                            Description = "This is a very important STORY blah blah blah...",
                            OwnerId = Guid.NewGuid(),
                            StoryPoints = 3,
                            WorkItemType = WorkItemType.Story,
                            WorkItemStatus = WorkItemStatus.Development,
                            CreatedAt = DateTime.Now.AddMonths(-3).ToShortDateString(),
                        },
                        new WorkItem
                        {
                            WorkItemId = workItemIdTwo,
                            Title = "Fix bug!",
                            Description = "This is a very important BUG",
                            OwnerId = Guid.NewGuid(),
                            StoryPoints = 5,
                            WorkItemType = WorkItemType.Bug,
                            WorkItemStatus = WorkItemStatus.Review,
                            CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                        },
                        new WorkItem
                        {
                            WorkItemId = workItemIdThree,
                            Title = "Fix another bug!",
                            Description = "This is the second BUG",
                            OwnerId = Guid.NewGuid(),
                            StoryPoints = 1,
                            WorkItemType = WorkItemType.Bug,
                            WorkItemStatus = WorkItemStatus.Review,
                            CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                        },
                        new WorkItem
                        {
                            WorkItemId = workItemIdFour,
                            Title = "Create UI for HomeFresh in React Native.",
                            Description = "Need to create very awesome design for the FE. Will need to use React Native.",
                            OwnerId = Guid.NewGuid(),
                            StoryPoints = 8,
                            WorkItemType = WorkItemType.Story,
                            WorkItemStatus = WorkItemStatus.Development,
                            CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                        }
                    };
        }
        private void GetProjects()
        {

            var projectIdOne = Guid.NewGuid();
            var projectIdTwo = Guid.NewGuid();
            var projectIdThree = Guid.NewGuid();
            var projectIdFour = Guid.NewGuid();

            Projects = new List<Project>
            {
                new Project
                {
                    Id = projectIdOne,
                    Title = "ESummit Server",
                    Description = "Web server for the coolest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-3).ToShortDateString(),
                    ProjectType = ProjectType.WebServer,
                    ProjectStatus = ProjectStatus.OnHold,
                },
                new Project
                {
                    Id = projectIdTwo,
                    Title = "ESummit Client",
                    Description = "Web client for the awesomest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                    ProjectType = ProjectType.WebClient,
                    ProjectStatus = ProjectStatus.OnHold,
                },
                new Project
                {
                    Id = projectIdThree,
                    Title = "Home Fresh",
                    Description = "Best mobile app for home made food!",
                    CreatedAt = DateTime.Now.AddMonths(-1).ToShortDateString(),
                    ProjectType = ProjectType.Mobile,
                    ProjectStatus = ProjectStatus.New,
                },
                new Project
                {
                    Id = projectIdFour,
                    Title = "Dating App",
                    Description = "Best dating app of all time!",
                    CreatedAt = DateTime.Now.AddMonths(-1).ToShortDateString(),
                    ProjectType = ProjectType.Mobile,
                    ProjectStatus = ProjectStatus.New,
                }
            };
        }
    }
}
