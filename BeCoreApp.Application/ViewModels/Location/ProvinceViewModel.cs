using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Location
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [MaxLength(256)]
        public string SeoPageTitle { set; get; }
        [MaxLength(256)]
        public string SeoAlias { set; get; }
        [MaxLength(256)]
        public string SeoKeywords { set; get; }
        [MaxLength(256)]
        public string SeoDescription { set; get; }

        public ICollection<DistrictViewModel> DistrictVMs { set; get; }
        public ICollection<WardViewModel> WardVMs { set; get; }
        public ICollection<StreetViewModel> StreetVMs { set; get; }
        public ICollection<EnterpriseViewModel> EnterpriseVMs { set; get; }
        public ICollection<ProjectViewModel> ProjectVMs { set; get; }
    }
}
