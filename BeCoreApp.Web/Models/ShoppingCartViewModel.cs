using BeCoreApp.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Models
{
    public class ShoppingCartViewModel
    {
        public ProductViewModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public ColorViewModel Color { get; set; }

        public SizeViewModel Size { get; set; }
    }
}
