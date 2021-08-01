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
    [Table("Districts")]
    public class District : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public District()
        {
            Wards = new List<Ward>();
            Streets = new List<Street>();
            Enterprises = new List<Enterprise>();
            Projects = new List<Project>();
        }

        public District(int id, string name, int provinceId, Status status, string seoPageTitle,
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
            Wards = new List<Ward>();
            Streets = new List<Street>();
            Enterprises=new List<Enterprise>();
            Projects = new List<Project>();
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

        [ForeignKey("ProvinceId")]
        public virtual Province Province { set; get; }
        public virtual ICollection<Ward> Wards { set; get; }
        public virtual ICollection<Street> Streets { set; get; }
        public virtual ICollection<Enterprise> Enterprises { set; get; }
        public virtual ICollection<Project> Projects { set; get; }
    }
}
