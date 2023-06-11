using Magnify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Data
{
    public interface IProjectDataProvider
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
    }

    public class ProjectDataProvider : IProjectDataProvider
    {
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            await Task.Delay(100); // Simulate a bit of server work

            return new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Title = "ESummit Server",
                    Description = "Web server for the coolest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-3).ToShortDateString(),
                    ProjectType = ProjectType.WebServer,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 2,
                    Title = "ESummit Client",
                    Description = "Web client for the awesomest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                    ProjectType = ProjectType.WebClient,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 3,
                    Title = "Home Fresh",
                    Description = "Best mobile app for home made food!",
                    CreatedAt = DateTime.Now.AddMonths(-1).ToShortDateString(),
                    ProjectType = ProjectType.Mobile,
                    ProjectStatus = ProjectStatus.New,
                },
                new Project
                {
                    Id = 4,
                    Title = "ESummit Server",
                    Description = "Web server for the coolest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-3).ToShortDateString(),
                    ProjectType = ProjectType.WebServer,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 5,
                    Title = "ESummit Client",
                    Description = "Web client for the awesomest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                    ProjectType = ProjectType.WebClient,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 6,
                    Title = "Home Fresh",
                    Description = "Best mobile app for home made food!",
                    CreatedAt = DateTime.Now.AddMonths(-1).ToShortDateString(),
                    ProjectType = ProjectType.Mobile,
                    ProjectStatus = ProjectStatus.New,
                },
                new Project
                {
                    Id = 7,
                    Title = "ESummit Server",
                    Description = "Web server for the coolest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-3).ToShortDateString(),
                    ProjectType = ProjectType.WebServer,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 8,
                    Title = "ESummit Client",
                    Description = "Web client for the awesomest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                    ProjectType = ProjectType.WebClient,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 9,
                    Title = "Home Fresh",
                    Description = "Best mobile app for home made food!",
                    CreatedAt = DateTime.Now.AddMonths(-1).ToShortDateString(),
                    ProjectType = ProjectType.Mobile,
                    ProjectStatus = ProjectStatus.New,
                },
                new Project
                {
                    Id = 10,
                    Title = "ESummit Server",
                    Description = "Web server for the coolest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-3).ToShortDateString(),
                    ProjectType = ProjectType.WebServer,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 11,
                    Title = "ESummit Client",
                    Description = "Web client for the awesomest online shopping site.",
                    CreatedAt = DateTime.Now.AddMonths(-2).ToShortDateString(),
                    ProjectType = ProjectType.WebClient,
                    ProjectStatus = ProjectStatus.Development,
                },
                new Project
                {
                    Id = 12,
                    Title = "Home Fresh",
                    Description = "Best mobile app for home made food!",
                    CreatedAt = DateTime.Now.AddMonths(-1).ToShortDateString(),
                    ProjectType = ProjectType.Mobile,
                    ProjectStatus = ProjectStatus.New,
                },
            };
        }
    }
}
