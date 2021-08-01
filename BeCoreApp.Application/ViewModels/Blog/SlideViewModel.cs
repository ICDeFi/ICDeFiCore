using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Blog
{
    public class SlideViewModel
    {
        public int Id { set; get; }
        [Required]
        [StringLength(250)]
        public string Name { set; get; }

        [StringLength(500)]
        public string Description { set; get; }

        [Required]
        [StringLength(250)]
        public string Image { set; get; }

        public string Url { get; set; }

        public bool? HotFlag { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
    }
}