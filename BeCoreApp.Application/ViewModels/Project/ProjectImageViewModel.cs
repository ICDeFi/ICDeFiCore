using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Project
{
    public class ProjectImageViewModel
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Caption { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public virtual ProjectViewModel ProjectVM { get; set; }
    }
}
