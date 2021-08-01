using BeCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Enterprise
{
    public class EnterpriseFieldViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public int EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
        [Required]
        public int FieldId { get; set; }
        public string FieldName { get; set; }

        public EnterpriseViewModel EnterpriseVM { set; get; }
        public FieldViewModel FieldVM { set; get; }
    }
}
