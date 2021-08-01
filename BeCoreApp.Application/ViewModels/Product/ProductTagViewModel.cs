
using BeCoreApp.Application.ViewModels.Common;

namespace BeCoreApp.Application.ViewModels.Product
{
    public class ProductTagViewModel
    {
        public int Id { get; set; }
        public int ProductId { set; get; }
        public string TagId { set; get; }

        public ProductViewModel Product { get; set; }
        public TagViewModel Tag { get; set; }
    }
}