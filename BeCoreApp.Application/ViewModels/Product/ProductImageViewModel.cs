
namespace BeCoreApp.Application.ViewModels.Product
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        
        public string Path { get; set; }

        public string Caption { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
