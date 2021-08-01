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
    [Table("ContactDetails")]
    public class Contact : DomainEntity<string>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Contact() { }

        public Contact(string id, string name, string phone, string email,
            string website, string address, string other, double? longtitude, double? latitude
            , Status status, string seoPageTitle,
               string seoAlias, string seoMetaKeyword,
               string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Website = website;
            Address = address;
            Other = other;
            Lng = longtitude;
            Lat = latitude;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
        }
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(50)]
        public string Phone { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(250)]
        public string Website { set; get; }

        [StringLength(250)]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
    }
}
