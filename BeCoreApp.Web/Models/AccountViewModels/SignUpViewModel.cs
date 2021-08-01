using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models.AccountViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Required Input User Reference")]
        [Display(Name = "User Reference")]
        public string UserReferenceId { get; set; }

        [Required(ErrorMessage = "Required Input User Reference Name")]
        [Display(Name = "User Reference Name")]
        public string UserReferenceName { get; set; }
    }
}
