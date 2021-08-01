using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Project;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.ProjectViewModels
{
    public class DetailViewModel
    {
        public DetailViewModel()
        {
            ProjectImageVMs = new List<ProjectImageViewModel>();
            ProjectLibraryVMs = new List<ProjectLibraryViewModel>();
        }

        public ProjectCategoryViewModel ProjectCategoryVM { get; set; }
        public ProjectViewModel ProjectVM { get; set; }
        public List<ProjectImageViewModel> ProjectImageVMs { get; set; } 
        public List<ProjectLibraryViewModel> ProjectLibraryVMs { get; set; }
        public List<TagViewModel> Tags { set; get; }
    }
}
