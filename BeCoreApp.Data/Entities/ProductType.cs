using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("ProductTypes")]
    public class ProductType : DomainEntity<string>
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { set; get; }
    }
}
