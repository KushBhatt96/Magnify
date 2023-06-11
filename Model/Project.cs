using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
        public ProjectType ProjectType { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

    }

    public enum ProjectType
    {
        Desktop,
        WebClient,
        WebServer,
        Mobile,
        Game
    }

    public enum ProjectStatus
    {
        New,
        Development,
        CodeReview,
        Testing,
        Resolved
    }
}
