using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        PagedResult<ProductViewModel> GetAllPaging(string startDate, string endDate, string keyword, int blogCategoryId, int page, int pageSize);
        PagedResult<ProductViewModel> GetAllByTagId(string tagId, int pageIndex, int pageSize);
        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        ProductViewModel GetById(int id);

        void ImportExcel(string filePath, int categoryId);

        void Save();

        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);

        void AddImages(int productId, string[] images);

        List<ProductImageViewModel> GetImages(int productId);

        void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices);

        List<WholePriceViewModel> GetWholePrices(int productId);

        List<ProductViewModel> GetRelatedProducts(int id, int top);

        List<ProductViewModel> GetUpsellProducts(int top);

        List<TagViewModel> GetProductTags(int productId);

        List<TagViewModel> GetListTagByType(string tagType);
        List<ProductTypeViewModel> GetAllProductType();
    }
}
