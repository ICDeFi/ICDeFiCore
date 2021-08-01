using BeCoreApp.Data.Enums;
using BeCoreApp.Data.Interfaces;
using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("Streets")]
    public class Street : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Street()
        {
        }

        public Street(int id, string name, int provinceId, int districtId, int wardId,
               Status status, string seoPageTitle,
               string seoAlias, string seoMetaKeyword,
               string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            ProvinceId = provinceId;
            DistrictId = districtId;
            WardId = wardId;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }

        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int WardId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { set; get; }

        [ForeignKey("DistrictId")]
        public virtual District District { set; get; }

        [ForeignKey("WardId")]
        public virtual Ward Ward { set; get; }
    }
}
