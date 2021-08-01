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
    [Table("Projects")]
    public class Project : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Project()
        {
            ProjectImages = new List<ProjectImage>();
            ProjectLibraries = new List<ProjectLibrary>();
        }

        public Project(int id, string name, string image,
            string totalArea, string areageBuild, string progressBuild, string projectScale, string handOverTheHouse,
            string content, string location, string infrastructure, string overallDiagram, string groundDesign, string video, string financialSupport,
            string address, bool? homeFlag, int provinceId, int districtId, int wardId, int projectCategoryId, int enterpriseId, Status status
            , string seoPageTitle,
               string seoAlias, string seoMetaKeyword,
               string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Image = image;
            TotalArea = totalArea;
            AreageBuild = areageBuild;
            ProgressBuild = progressBuild;
            ProjectScale = projectScale;
            HandOverTheHouse = handOverTheHouse;
            Content = content;
            Location = location;
            Infrastructure = infrastructure;
            GroundDesign = groundDesign;
            OverallDiagram = overallDiagram;
            Video = video;
            FinancialSupport = financialSupport;
            Address = address;
            HomeFlag = homeFlag;
            ProvinceId = provinceId;
            DistrictId = districtId;
            WardId = wardId;
            ProjectCategoryId = projectCategoryId;
            EnterpriseId = enterpriseId;
            Status = Status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            ProjectImages = new List<ProjectImage>();
            ProjectLibraries = new List<ProjectLibrary>();
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(255)]
        public string TotalArea { get; set; }//tổng diện tích

        [StringLength(255)]
        public string AreageBuild { get; set; }//diện tích xây dựng

        [StringLength(255)]
        public string ProgressBuild { get; set; }//tiến độ thực hiện

        [StringLength(255)]
        public string ProjectScale { get; set; }//quy mô dự án

        [StringLength(255)]
        public string HandOverTheHouse { get; set; }//bàn giao nhà

        public string Content { get; set; }//giới thiệu tổng quan

        public string Location { get; set; }//vị trí

        public string Infrastructure { get; set; }//hạ tầng

        public string OverallDiagram { get; set; }//sơ đồ tổng thể

        public string GroundDesign { get; set; }//thiết kế mặt bằng

        public string Video { set; get; }

        public string FinancialSupport { get; set; }//hỗ trợ tài chính


        [StringLength(250)]
        public string Address { set; get; }

        public bool? HomeFlag { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public int WardId { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        [Required]
        public int EnterpriseId { get; set; }

        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { set; get; }

        [ForeignKey("DistrictId")]
        public virtual District District { set; get; }

        [ForeignKey("WardId")]
        public virtual Ward Ward { set; get; }

        [ForeignKey("ProjectCategoryId")]
        public virtual ProjectCategory ProjectCategory { set; get; }

        [ForeignKey("EnterpriseId")]
        public virtual Enterprise Enterprise { set; get; }

        public virtual ICollection<ProjectImage> ProjectImages { set; get; }

        public virtual ICollection<ProjectLibrary> ProjectLibraries { set; get; }

    }
}
