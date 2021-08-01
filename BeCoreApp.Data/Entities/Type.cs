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
    [Table("Types")]
    public class Type : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Type()
        {
            Units = new List<Unit>();
            ClassifiedCategories = new List<ClassifiedCategory>();
        }

        public Type(int id, string name, Status status, string seoPageTitle,
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
            Units = new List<Unit>();
            ClassifiedCategories = new List<ClassifiedCategory>();
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

        public virtual ICollection<Unit> Units { set; get; }
        public virtual ICollection<ClassifiedCategory> ClassifiedCategories { set; get; }
    }
}
