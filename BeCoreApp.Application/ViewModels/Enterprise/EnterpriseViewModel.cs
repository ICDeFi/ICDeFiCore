using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Enterprise
{
    public class EnterpriseViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [StringLength(255)]
        public string Image { get; set; }
        public string Content { get; set; }
        [StringLength(50)]
        public string Phone { set; get; }
        [StringLength(250)]
        public string Email { set; get; }
        [StringLength(250)]
        public string Website { set; get; }
        [StringLength(250)]
        public string Address { set; get; }

        public string Hotline { get; set; }
        public bool? HomeFlag { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        [Required]
        public int WardId { get; set; }
        public string WardName { get; set; }
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

        public virtual ProvinceViewModel ProvinceVM { set; get; }
        public virtual DistrictViewModel DistrictVM { set; get; }
        public virtual WardViewModel WardVM { set; get; }
        public ICollection<EnterpriseFieldViewModel> EnterpriseFieldVMs { set; get; }
        public ICollection<ProjectViewModel> ProjectVMs { set; get; }
    }
}
