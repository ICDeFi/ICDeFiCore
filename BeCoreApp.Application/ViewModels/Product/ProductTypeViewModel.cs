
using BeCoreApp.Application.ViewModels.Common;
using System.Collections.Generic;

namespace BeCoreApp.Application.ViewModels.Product
{
    public class ProductTypeViewModel
    {
        public ProductTypeViewModel()
        {
            Products = new List<ProductViewModel>();
        }
        public string Id { get; set; }
        public string Name { set; get; }

        public List<ProductViewModel> Products { set; get; }
    }
}