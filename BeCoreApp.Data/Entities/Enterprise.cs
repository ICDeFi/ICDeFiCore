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
    [Table("Enterprises")]
    public class Enterprise : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Enterprise()
        {
            EnterpriseFields = new List<EnterpriseField>();
            Projects = new List<Project>();
        }

        public Enterprise(int id, string name, string image,
            string content, string phone, string email, string website,
            string address, string hotline, bool? homeFlag, int provinceId, int districtId, int wardId
            , Status status, string seoPageTitle,
               string seoAlias, string seoMetaKeyword,
               string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Image = image;
            Content = content;
            Phone = phone;
            Email = email;
            Website = website;
            Address = address;
            Hotline = hotline;
            HomeFlag = homeFlag;
            ProvinceId = provinceId;
            DistrictId = districtId;
            WardId = wardId;
            Status = Status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            EnterpriseFields = new List<EnterpriseField>();
            Projects = new List<Project>();
        }

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
        public Status Status { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

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
        public virtual ICollection<EnterpriseField> EnterpriseFields { set; get; }
        public virtual ICollection<Project> Projects { set; get; }
    }
}
