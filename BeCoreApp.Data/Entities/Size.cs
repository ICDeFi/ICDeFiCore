using BeCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeCoreApp.Data.Entities
{
    [Table("Sizes")]
    public class Size : DomainEntity<int>
    {

        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<ProductQuantity> ProductQuantities { set; get; }
    }
}
