using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.RealEstate
{
    public class ClassifiedCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [Required]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
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

        public TypeViewModel TypeVM { set; get; }
    }
}
