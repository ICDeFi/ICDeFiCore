using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public string Image { get; set; }

        public string TotalArea { get; set; }//tổng diện tích

        public string AreageBuild { get; set; }//diện tích xây dựng

        public string ProgressBuild { get; set; }//tiến độ thực hiện

        public string ProjectScale { get; set; }//quy mô dự án

        public string HandOverTheHouse { get; set; }//bàn giao nhà

        public string Content { get; set; }//giới thiệu tổng quan

        public string Location { get; set; }//vị trí

        public string Infrastructure { get; set; }//hạ tầng

        public string OverallDiagram { get; set; }//sơ đồ tổng thể

        public string GroundDesign { get; set; }//thiết kế mặt bằng

        public string Video { set; get; }

        public string FinancialSupport { get; set; }//hỗ trợ tài chính

        public string Address { set; get; }

        public bool? HomeFlag { get; set; }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int WardId { get; set; }
        public string WardName { get; set; }

        public int ProjectCategoryId { get; set; }
        public string ProjectCategoryName { get; set; }

        public int EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }

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

        public virtual ProjectCategoryViewModel ProjectCategoryVM { set; get; }

        public virtual EnterpriseViewModel EnterpriseVM { set; get; }

        public ICollection<ProjectImageViewModel> ProjectImageVMs { set; get; }

        public ICollection<ProjectLibraryViewModel> ProjectLibraryVMs { set; get; }

    }
}
