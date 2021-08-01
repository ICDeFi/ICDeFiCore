using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Common
{
    public class SupportViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string RequestContent { set; get; }
        public string ResponseContent { set; get; }
        public SupportType Type { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Guid AppUserId { get; set; }
        public string AppUserName { get; set; }

        public  AppUserViewModel AppUser { set; get; }
    }
}
