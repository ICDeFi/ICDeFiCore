using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Location
{
    public class StreetViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
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

        public ProvinceViewModel ProvinceVM { set; get; }
        public DistrictViewModel DistrictVM { set; get; }
        public WardViewModel WardVM { set; get; }
    }
}
