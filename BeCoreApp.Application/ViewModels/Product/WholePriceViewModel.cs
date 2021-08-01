using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.Product
{
    public class WholePriceViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int FromQuantity { get; set; }

        public int ToQuantity { get; set; }

        public decimal Price { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
