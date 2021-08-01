using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IProductRepository _productRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IProductQuantityRepository _productQuantityRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IWholePriceRepository _wholePriceRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IBlogCategoryService blogCategoryService,
            ITagRepository tagRepository, IProductQuantityRepository productQuantityRepository,
            IProductImageRepository productImageRepository, IWholePriceRepository wholePriceRepository,
            IUnitOfWork unitOfWork, IProductTypeRepository productTypeRepository, IProductTagRepository productTagRepository)
        {
            _productTypeRepository = productTypeRepository;
            _blogCategoryService = blogCategoryService;
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productQuantityRepository = productQuantityRepository;
            _productTagRepository = productTagRepository;
            _productImageRepository = productImageRepository;
            _wholePriceRepository = wholePriceRepository;
            _unitOfWork = unitOfWork;
        }

        public void CheckSeo(ProductViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            modeVm.SeoAlias = TextHelper.UrlFriendly(modeVm.Name);
        }

        public ProductViewModel Add(ProductViewModel productVm)
        {
            CheckSeo(productVm);
            var product = new Product
            {
                Id = productVm.Id,
                Name = productVm.Name,
                Image = productVm.Image,
                PromotionPrice = productVm.PromotionPrice,
                Price = productVm.Price,
                OriginalPrice = productVm.OriginalPrice,
                Description = productVm.Description,
                MildContent = productVm.MildContent,
                HomeFlag = productVm.HomeFlag,
                HotFlag = productVm.HotFlag,
                Status = productVm.Status,
                SeoPageTitle = productVm.SeoPageTitle,
                SeoAlias = productVm.SeoAlias,
                SeoKeywords = productVm.SeoKeywords,
                SeoDescription = productVm.SeoDescription,
                BlogCategoryId = productVm.BlogCategoryId,
                ViewCount = 10
            };

            if (productVm.Tags.Count() > 0)
            {
                product.ProductTags = new List<ProductTag>();

                foreach (string t in productVm.Tags.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId && x.Type == CommonConstants.ProductTag).Any())
                    {
                        _tagRepository.Add(new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        });
                    }

                    product.ProductTags.Add(new ProductTag
                    {
                        TagId = tagId
                    });
                }
            }


            var productTypeId = TextHelper.ToUnsignString(productVm.ProductTypeId);
            if (!_productTypeRepository.FindAll(x => x.Id == productTypeId).Any())
            {
                _productTypeRepository.Add(new ProductType
                {
                    Id = productTypeId,
                    Name = productVm.ProductTypeId
                });
            }

            product.ProductTypeId = productTypeId;

            _productRepository.Add(product);
            return productVm;
        }

        public void AddQuantity(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productQuantityRepository.RemoveMultiple(_productQuantityRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var quantity in quantities)
            {
                _productQuantityRepository.Add(new ProductQuantity()
                {
                    ProductId = productId,
                    ColorId = quantity.ColorId,
                    SizeId = quantity.SizeId,
                    Quantity = quantity.Quantity
                });
            }
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<ProductViewModel> GetAllPaging(string startDate, string endDate, string keyword, int blogCategoryId, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.BlogCategory, a => a.ProductTags, pt => pt.ProductType, pm => pm.ProductImages);
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated >= start);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated <= end);
            }

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            if (blogCategoryId != 0)
            {
                var blogCategoryList = _blogCategoryService.GetBlogCategoryIdsById(blogCategoryId, MenuFrontEndType.SanPham);
                query = query.Where(x => blogCategoryList.Contains(x.BlogCategoryId));
            }

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    PromotionPrice = x.PromotionPrice,
                    Price = x.Price,
                    OriginalPrice = x.OriginalPrice,
                    Description = x.Description,
                    MildContent = x.MildContent,
                    HomeFlag = x.HomeFlag,
                    HotFlag = x.HotFlag,
                    Status = x.Status,
                    SeoPageTitle = x.SeoPageTitle,
                    SeoAlias = x.SeoAlias,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    BlogCategoryId = x.BlogCategoryId,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    ViewCount = x.ViewCount,
                    ProductTypeId = x.ProductTypeId,
                    BlogCategory = new BlogCategoryViewModel() { Id = x.BlogCategory.Id, Name = x.BlogCategory.Name },
                    ProductType = new ProductTypeViewModel() { Id = x.ProductType.Id, Name = x.ProductType.Name },
                    ProductImages = x.ProductImages.OrderBy(f => f.Id)
                                     .Select(pm => new ProductImageViewModel()
                                     {
                                         Id = pm.Id,
                                         Path = pm.Path
                                     }).ToList(),
                    ProductTags = x.ProductTags
                                   .Select(pt => new ProductTagViewModel()
                                   {
                                       Id = pt.Id,
                                       TagId = pt.TagId,
                                       ProductId = pt.ProductId,
                                       Tag = new TagViewModel()
                                       {
                                           Id = pt.Tag.Id,
                                           Name = pt.Tag.Name,
                                           Type = pt.Tag.Type
                                       }
                                   }).ToList()
                }).ToList();

            return new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }

        public PagedResult<ProductViewModel> GetAllByTagId(string tagId, int pageIndex, int pageSize)
        {
            var query = from p in _productRepository.FindAll(x => x.BlogCategory, a => a.ProductTags, pt => pt.ProductType, pm => pm.ProductImages)
                        join pt in _productTagRepository.FindAll()
                        on p.Id equals pt.ProductId
                        where pt.TagId == tagId && p.Status == Status.Active
                        orderby p.DateCreated descending
                        select p;

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    PromotionPrice = x.PromotionPrice,
                    Price = x.Price,
                    OriginalPrice = x.OriginalPrice,
                    Description = x.Description,
                    MildContent = x.MildContent,
                    HomeFlag = x.HomeFlag,
                    HotFlag = x.HotFlag,
                    Status = x.Status,
                    SeoPageTitle = x.SeoPageTitle,
                    SeoAlias = x.SeoAlias,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    BlogCategoryId = x.BlogCategoryId,
                    BlogCategoryName = x.BlogCategory.Name,
                    DateModified = x.DateModified,
                    DateCreated = x.DateCreated,
                    ViewCount = x.ViewCount,
                    ProductTypeId = x.ProductTypeId,
                    BlogCategory = new BlogCategoryViewModel() { Id = x.BlogCategory.Id, Name = x.BlogCategory.Name },
                    ProductType = new ProductTypeViewModel() { Id = x.ProductType.Id, Name = x.ProductType.Name },
                    ProductImages = x.ProductImages.OrderByDescending(f => f.Id)
                                     .Select(pm => new ProductImageViewModel()
                                     {
                                         Id = pm.Id,
                                         Path = pm.Path
                                     }).ToList(),
                    ProductTags = x.ProductTags
                                   .Select(pt => new ProductTagViewModel()
                                   {
                                       Id = pt.Id,
                                       TagId = pt.TagId,
                                       ProductId = pt.ProductId,
                                       Tag = new TagViewModel()
                                       {
                                           Id = pt.Tag.Id,
                                           Name = pt.Tag.Name,
                                           Type = pt.Tag.Type
                                       }
                                   }).ToList()
                }).ToList();

            return new PagedResult<ProductViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public ProductViewModel GetById(int id)
        {
            var model = _productRepository.FindById(id, x => x.BlogCategory, a => a.ProductTags, pt => pt.ProductType, pm => pm.ProductImages);
            if (model == null)
                return null;

            return new ProductViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                PromotionPrice = model.PromotionPrice,
                Price = model.Price,
                OriginalPrice = model.OriginalPrice,
                Description = model.Description,
                MildContent = model.MildContent,
                HomeFlag = model.HomeFlag,
                HotFlag = model.HotFlag,
                Status = model.Status,
                SeoPageTitle = model.SeoPageTitle,
                SeoAlias = model.SeoAlias,
                SeoKeywords = model.SeoKeywords,
                SeoDescription = model.SeoDescription,
                BlogCategoryId = model.BlogCategoryId,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
                ViewCount = model.ViewCount,
                ProductTypeId = model.ProductTypeId,
                ProductImages = model.ProductImages.OrderByDescending(f => f.Id)
                                     .Select(pm => new ProductImageViewModel()
                                     {
                                         Id = pm.Id,
                                         Path = pm.Path
                                     }).ToList(),
                BlogCategory = new BlogCategoryViewModel()
                {
                    Id = model.BlogCategory.Id,
                    Name = model.BlogCategory.Name
                },
                ProductType = new ProductTypeViewModel()
                {
                    Id = model.ProductType.Id,
                    Name = model.ProductType.Name
                },
                ProductTags = model.ProductTags
                                   .Select(x => new ProductTagViewModel()
                                   {
                                       Id = x.Id,
                                       TagId = x.TagId,
                                       ProductId = x.ProductId,
                                   }).ToList()
            };
        }

        public List<ProductQuantityViewModel> GetQuantities(int productId)
        {
            return _productQuantityRepository.FindAll(x => x.ProductId == productId,
                x => x.Product, x => x.Size, x => x.Color)
                .Select(x => new ProductQuantityViewModel()
                {
                    ColorId = x.ColorId,
                    ProductId = x.ProductId,
                    SizeId = x.SizeId,
                    Quantity = x.Quantity,
                    ProductName = x.Product.Name,
                    SizeName = x.Size.Name,
                    ColorName = x.Color.Name
                }).ToList();
        }

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

                    product.PromotionPrice = promotionPrice;
                    product.MildContent = workSheet.Cells[i, 6].Value.ToString();
                    product.SeoKeywords = workSheet.Cells[i, 7].Value.ToString();

                    product.SeoDescription = workSheet.Cells[i, 8].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    _productRepository.Add(product);
                }
            }
        }

        public void Save() => _unitOfWork.Commit();

        public void Update(ProductViewModel productVm)
        {
            CheckSeo(productVm);

            var product = _productRepository.FindById(productVm.Id);
            product.Id = productVm.Id;
            product.Name = productVm.Name;
            product.Image = productVm.Image;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Price = productVm.Price;
            product.OriginalPrice = productVm.OriginalPrice;
            product.Description = productVm.Description;
            product.MildContent = productVm.MildContent;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.Status = productVm.Status;
            product.SeoPageTitle = productVm.SeoPageTitle;
            product.SeoAlias = productVm.SeoAlias;
            product.SeoKeywords = productVm.SeoKeywords;
            product.SeoDescription = productVm.SeoDescription;
            product.BlogCategoryId = productVm.BlogCategoryId;

            var productTagRemoves = _productTagRepository.FindAll(x => x.ProductId == productVm.Id).ToList();
            _productTagRepository.RemoveMultiple(productTagRemoves);

            if (productVm.Tags.Count() > 0)
            {
                foreach (string t in productVm.Tags.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId && x.Type == CommonConstants.ProductTag).Any())
                    {
                        _tagRepository.Add(new Tag()
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        });
                    }

                    _productTagRepository.Add(new ProductTag { ProductId = productVm.Id, TagId = tagId });
                }
            }

            var productTypeId = TextHelper.ToUnsignString(productVm.ProductTypeId);
            if (!_productTypeRepository.FindAll(x => x.Id == productTypeId).Any())
            {
                _productTypeRepository.Add(new ProductType
                {
                    Id = productTypeId,
                    Name = productVm.ProductTypeId
                });
            }

            product.ProductTypeId = productTypeId;


            _productRepository.Update(product);
        }

        public List<ProductImageViewModel> GetImages(int productId)
        {
            return _productImageRepository.FindAll(x => x.ProductId == productId, x => x.Product)
                .Select(x => new ProductImageViewModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Path = x.Path,
                    Caption = x.Caption,
                    ProductName = x.Product.Name
                }).ToList();
        }

        public void AddImages(int productId, string[] images)
        {
            _productImageRepository.RemoveMultiple(_productImageRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var image in images)
            {
                _productImageRepository.Add(new ProductImage()
                {
                    Path = image,
                    ProductId = productId,
                    Caption = string.Empty
                });
            }
        }

        public void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices)
        {
            _wholePriceRepository.RemoveMultiple(_wholePriceRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var wholePrice in wholePrices)
            {
                _wholePriceRepository.Add(new WholePrice()
                {
                    ProductId = productId,
                    FromQuantity = wholePrice.FromQuantity,
                    ToQuantity = wholePrice.ToQuantity,
                    Price = wholePrice.Price
                });
            }
        }

        public List<WholePriceViewModel> GetWholePrices(int productId)
        {
            return _wholePriceRepository.FindAll(x => x.ProductId == productId, x => x.Product)
                .Select(x => new WholePriceViewModel()
                {
                    ProductName = x.Product.Name,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    FromQuantity = x.FromQuantity,
                    ToQuantity = x.ToQuantity
                }).ToList();
        }

        public List<ProductViewModel> GetRelatedProducts(int id, int top)
        {
            return _productRepository.FindAll(x => x.Status == Status.Active && x.Id != id)
            .OrderByDescending(x => x.DateCreated).Take(top)
            .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                PromotionPrice = x.PromotionPrice,
                Price = x.Price,
                OriginalPrice = x.OriginalPrice,
                Description = x.Description,
                MildContent = x.MildContent,
                HomeFlag = x.HomeFlag,
                HotFlag = x.HotFlag,
                Status = x.Status,
                SeoPageTitle = x.SeoPageTitle,
                SeoAlias = x.SeoAlias,
                SeoKeywords = x.SeoKeywords,
                SeoDescription = x.SeoDescription,
                BlogCategoryId = x.BlogCategoryId,
                BlogCategoryName = x.BlogCategory.Name,
                DateModified = x.DateModified,
                DateCreated = x.DateCreated,
                ViewCount = x.ViewCount,
                ProductTypeId = x.ProductTypeId,
                BlogCategory = new BlogCategoryViewModel() { Id = x.BlogCategory.Id, Name = x.BlogCategory.Name },
                ProductType = new ProductTypeViewModel() { Id = x.ProductType.Id, Name = x.ProductType.Name },
                ProductImages = x.ProductImages.OrderByDescending(f => f.Id)
                                     .Select(pm => new ProductImageViewModel()
                                     {
                                         Id = pm.Id,
                                         Path = pm.Path
                                     }).ToList(),
                ProductTags = x.ProductTags
                                   .Select(pt => new ProductTagViewModel()
                                   {
                                       Id = pt.Id,
                                       TagId = pt.TagId,
                                       ProductId = pt.ProductId,
                                       Tag = new TagViewModel()
                                       {
                                           Id = pt.Tag.Id,
                                           Name = pt.Tag.Name,
                                           Type = pt.Tag.Type
                                       }
                                   }).ToList()
            }).ToList();
        }

        public List<ProductViewModel> GetUpsellProducts(int top)
        {
            return _productRepository.FindAll(x => x.PromotionPrice != null)
               .OrderByDescending(x => x.DateModified).Take(top)
               .Select(x => new ProductViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Image = x.Image,
                   PromotionPrice = x.PromotionPrice,
                   Price = x.Price,
                   OriginalPrice = x.OriginalPrice,
                   Description = x.Description,
                   MildContent = x.MildContent,
                   HomeFlag = x.HomeFlag,
                   HotFlag = x.HotFlag,
                   Status = x.Status,
                   SeoPageTitle = x.SeoPageTitle,
                   SeoAlias = x.SeoAlias,
                   SeoKeywords = x.SeoKeywords,
                   SeoDescription = x.SeoDescription,
                   BlogCategoryId = x.BlogCategoryId,
                   BlogCategoryName = x.BlogCategory.Name,
                   DateModified = x.DateModified,
                   DateCreated = x.DateCreated,
                   ViewCount = x.ViewCount,
                   ProductTypeId = x.ProductTypeId,
                   BlogCategory = new BlogCategoryViewModel() { Id = x.BlogCategory.Id, Name = x.BlogCategory.Name },
                   ProductType = new ProductTypeViewModel() { Id = x.ProductType.Id, Name = x.ProductType.Name },
                   ProductImages = x.ProductImages.OrderByDescending(f => f.Id)
                                     .Select(pm => new ProductImageViewModel()
                                     {
                                         Id = pm.Id,
                                         Path = pm.Path
                                     }).ToList(),
                   ProductTags = x.ProductTags
                                   .Select(pt => new ProductTagViewModel()
                                   {
                                       Id = pt.Id,
                                       TagId = pt.TagId,
                                       ProductId = pt.ProductId,
                                       Tag = new TagViewModel()
                                       {
                                           Id = pt.Tag.Id,
                                           Name = pt.Tag.Name,
                                           Type = pt.Tag.Type
                                       }
                                   }).ToList()
               }).ToList();
        }

        public List<TagViewModel> GetProductTags(int productId)
        {
            var tags = _tagRepository.FindAll();
            var productTags = _productTagRepository.FindAll();

            var query = from t in tags
                        join pt in productTags
                        on t.Id equals pt.TagId
                        where pt.ProductId == productId
                        select new TagViewModel()
                        {
                            Id = t.Id,
                            Name = t.Name
                        };

            return query.ToList();

        }

        public List<TagViewModel> GetListTagByType(string tagType)
        {
            return _tagRepository.FindAll(x => x.Type == tagType)
                .Select(x => new TagViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                }).ToList();
        }

        public List<ProductTypeViewModel> GetAllProductType()
        {
            return _productTypeRepository.FindAll()
                .Select(x => new ProductTypeViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}
