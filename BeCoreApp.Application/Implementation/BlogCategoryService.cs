using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Extensions;
using BeCoreApp.Utilities.Helpers;

namespace BeCoreApp.Application.Implementation
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private IBlogCategoryRepository _blogCategoryRepository;
        public IFunctionService _functionService;
        private IUnitOfWork _unitOfWork;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IFunctionService functionService, IUnitOfWork unitOfWork)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _functionService = functionService;
            _unitOfWork = unitOfWork;
        }

        List<int> listData = new List<int>();
        public List<int> GetBlogCategoryIdsById(int id, MenuFrontEndType type)
        {
            listData = new List<int>();
            listData.Add(id);
            var menuItems = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId != null).ToList();
            GetBlogCategoryIdsByParentId(menuItems, id);

            return listData;
        }

        private void GetBlogCategoryIdsByParentId(List<BlogCategory> menuList, int? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);
            if (children.Count > 0)
            {
                foreach (var item in children)
                {
                    listData.Add(item.Id);
                    GetBlogCategoryIdsByParentId(menuList, item.Id);
                }
            }
        }

        public string GetHomeSidebarMenuStringByType(MenuFrontEndType type)
        {
            var menuItemParents = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId == null);
            if (menuItemParents.Count() == 0)
                return string.Empty;

            var menuItems = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId != null).ToList();

            var builder = new StringBuilder();
            builder.Append("<ul class=\"menu\">");
            foreach (var menuItemParent in menuItemParents)
            {
                builder.Append(GetHomeSidebarMenuListString(menuItems, menuItemParent.Id));
            }
            builder.Append("</ul>");
            return builder.ToString();

        }

        private string GetHomeSidebarMenuListString(List<BlogCategory> menuList, int? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (children.Count <= 0)
                return "";

            var builder = new StringBuilder();

            foreach (var item in children)
            {
                var childStr = GetHomeSidebarMenuListString(menuList, item.Id);
                if (!string.IsNullOrWhiteSpace(childStr))
                {
                    builder.AppendFormat("<li class=\"menu-item active-mega-menu\"><a href=\"{0}\"><i class=\"fa icofont icofont-broccoli\"></i>{1}</a>", item.URL, item.Name);
                    builder.Append("<div class=\"dropdown-menu\">");
                    builder.Append("<div class=\"dropdown-menu-inner\">");
                    builder.Append("<div class=\"tbay_custom_menu\">");
                    builder.Append("<div class=\"widget widget_nav_menu\">");
                    builder.Append("<ul class=\"menu\">");
                    builder.Append(childStr);
                    builder.Append("</ul></div></div></div></div></li>");
                }
                else
                    builder.AppendFormat("<li class=\"menu-item\"><a href=\"{0}\"><i class=\"fa icofont icofont-broccoli\"></i>{1}</a></li>", item.URL, item.Name);
            }

            return builder.ToString();
        }

        public string GetMobileMenuStringByType(MenuFrontEndType type)
        {
            var menuItemParents = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId == null);
            if (menuItemParents.Count() == 0)
                return string.Empty;

            var menuItems = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId != null).ToList();

            var builder = new StringBuilder();
            builder.Append("<ul id=\"main-mobile-menu\" class=\"menu treeview nav navbar-nav\">");
            foreach (var menuItemParent in menuItemParents)
            {
                builder.Append(GetMobileMenuListString(menuItems, menuItemParent.Id));
            }
            builder.Append("</ul>");
            return builder.ToString();
        }

        private string GetMobileMenuListString(List<BlogCategory> menuList, int? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (children.Count <= 0)
                return "";

            var builder = new StringBuilder();

            foreach (var item in children)
            {
                var childStr = GetMobileMenuListString(menuList, item.Id);
                if (!string.IsNullOrWhiteSpace(childStr))
                {
                    builder.AppendFormat("<li class=\"menu-item\"><a href=\"{0}\">{1}</a>", item.URL, item.Name);
                    builder.Append("<ul class=\"dropdown-menu\">");
                    builder.Append(childStr);
                    builder.Append("</ul></li>");
                }
                else
                    builder.AppendFormat("<li class=\"menu-item\"><a href=\"{0}\">{1}</a></li>", item.URL, item.Name);
            }

            return builder.ToString();
        }

        public string GetMobileMainMenuStringByType(MenuFrontEndType type)
        {
            var menuItemParents = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId == null);
            if (menuItemParents.Count() == 0)
                return string.Empty;

            var menuItems = _blogCategoryRepository.FindAll(x => x.Type == type && x.ParentId != null).ToList();

            var builder = new StringBuilder();
            builder.Append("<ul class=\"menu\">");
            foreach (var menuItemParent in menuItemParents)
            {
                builder.Append(GetMobileMainMenuListString(menuItems, menuItemParent.Id));
            }
            builder.Append("</ul>");
            return builder.ToString();
        }

        public string GetMobileMainMenuString()
        {
            var menuItems = _blogCategoryRepository.FindAll().ToList();

            var builder = new StringBuilder();
            builder.Append("<ul class=\"menu\">");
            builder.Append(GetMobileMainMenuListString(menuItems, null));
            builder.Append("</ul>");
            return builder.ToString();
        }

        private string GetMobileMainMenuListString(List<BlogCategory> menuList, int? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (children.Count <= 0)
                return "";

            var builder = new StringBuilder();

            foreach (var item in children)
            {
                var childStr = GetMobileMainMenuListString(menuList, item.Id);
                if (!string.IsNullOrWhiteSpace(childStr))
                {
                    builder.AppendFormat("<li class=\"menu-item\"><a href=\"{0}\">{1}</a>", item.URL, item.Name);
                    builder.Append("<ul class=\"sub-menu\">");
                    builder.Append(childStr);
                    builder.Append("</ul></li>");
                }
                else
                    builder.AppendFormat("<li class=\"menu-item\"><a href=\"{0}\">{1}</a></li>", item.URL, item.Name);
            }

            return builder.ToString();
        }

        public string GetMainMenuString()
        {
            var menuItems = _blogCategoryRepository.FindAll().ToList();

            var builder = new StringBuilder();
            builder.Append("<ul class=\"horizontalMenu-list\">");
            builder.Append(GetMainMenuListString(menuItems, null));
            builder.Append("</ul>");
            return builder.ToString();
        }

        private string GetMainMenuListString(List<BlogCategory> menuList, int? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (children.Count <= 0)
                return "";

            var builder = new StringBuilder();

            foreach (var item in children)
            {
                var childStr = GetMainMenuListString(menuList, item.Id);
                if (!string.IsNullOrWhiteSpace(childStr))
                {
                    builder.Append("<li aria-haspopup=\"true\" class=\"sub-menu-sub\">");
                    builder.AppendFormat("<a class=\"sub-icon\" href=\"{0}\">{1}</a>", item.URL, item.Name);
                    builder.Append("<ul class=\"sub-menu\">");
                    builder.Append(childStr);
                    builder.Append("</ul></li>");
                }
                else
                    builder.AppendFormat("<li aria-haspopup=\"true\"><a href=\"{0}\" class=\"slide-item\">{1}</a></li>", item.URL, item.Name);
            }

            return builder.ToString();
        }

        private List<BlogCategory> GetChildrenMenu(List<BlogCategory> menuList, int? parentId)
        {
            return menuList.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder).ToList();
        }

        public List<BlogCategoryTreeViewModel> GetTreeAll()
        {
            var listData = _blogCategoryRepository.FindAll()
                .Select(x => new BlogCategoryTreeViewModel()
            {
                id = x.Id,
                text = x.Name,
                icon = "fa fa-folder",
                state = new BlogCategoryTreeState { opened = true },
                data = new BlogCategoryTreeData
                {
                    rootId = x.Id,
                    description = x.Description,
                    parentId = x.ParentId,
                    sortOrder = x.SortOrder,
                    seoAlias = x.SeoAlias,
                    seoPageTitle = x.SeoPageTitle,
                    seoKeywords = x.SeoKeywords,
                    seoDescription = x.SeoDescription,
                    functionId = x.FunctionId,
                    functionName = x.Function.Name,
                    url = x.URL,
                    status = x.Status,
                    isMain = x.IsMain,
                    iconCss = x.IconCss,
                    type = x.Type,
                    typeName = x.Type.GetDescription()
                }
            });

            if (listData.Count() == 0)
                return new List<BlogCategoryTreeViewModel>();

            var groups = listData.GroupBy(i => i.data.parentId);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue)
                    .ToDictionary(g => g.Key.Value, g => g.ToList());

                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        private void AddChildren(BlogCategoryTreeViewModel root, IDictionary<int, List<BlogCategoryTreeViewModel>> source)
        {
            if (source.ContainsKey(root.id))
            {
                root.children = source[root.id].OrderBy(x => x.data.sortOrder).ToList();
                for (int i = 0; i < root.children.Count; i++)
                    AddChildren(root.children[i], source);
            }
            else
            {
                root.icon = "fa fa-file m--font-warning";
                root.children = new List<BlogCategoryTreeViewModel>();
            }
        }

        public void UpdateParentId(int id, int? newParentId)
        {
            var blogCategory = _blogCategoryRepository.FindById(id);
            blogCategory.SortOrder = 1;
            blogCategory.ParentId = newParentId;

            var blogCategorydbs = _blogCategoryRepository.FindAll(x => x.ParentId == newParentId);
            if (blogCategorydbs.Count() > 0)
                blogCategory.SortOrder += blogCategorydbs.Max(x => x.SortOrder);

            _blogCategoryRepository.Update(blogCategory);
        }
        public List<BlogCategoryViewModel> GetMainItems()
        {
            var models = new List<BlogCategoryViewModel>();

            models = _blogCategoryRepository.FindAll(x => x.IsMain == Status.Active)
               .Select(x => new BlogCategoryViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   URL = x.URL,
               }).ToList();

            return models;
        }
        public List<BlogCategoryViewModel> SideBarCategoryByType(MenuFrontEndType type)
        {
            var models = new List<BlogCategoryViewModel>();

            var listCategory = _blogCategoryRepository.FindAll(x => x.Type == type, x => x.Blogs).ToList();

            var listCategoryChild = _blogCategoryRepository.FindAll(x => x.Type == MenuFrontEndType.BaiViet, x => x.Blogs).ToList();

            models = listCategory.Where(x => x.ParentId == null)
                .Select(x => new BlogCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    URL = x.URL,
                    CountBlog = x.Blogs.Count(),
                    BlogCategoryChild = listCategoryChild.Where(bc => bc.ParentId == x.Id)
                    .Select(cd => new BlogCategoryViewModel
                    {
                        Id = cd.Id,
                        Name = cd.Name,
                        Description = cd.Description,
                        URL = cd.URL,
                        CountBlog = cd.Blogs.Count()
                    }).ToList()
                }).ToList();

            return models;
        }

        public BlogCategoryViewModel GetById(int id)
        {
            var blogCategory = _blogCategoryRepository.FindById(id, x => x.Function);
            if (blogCategory == null)
                return null;

            return new BlogCategoryViewModel
            {
                Id = blogCategory.Id,
                Name = blogCategory.Name,
                Description = blogCategory.Description,
                FunctionId = blogCategory.FunctionId,
                FunctionName = blogCategory.Function.Name,
                ParentId = blogCategory.ParentId,
                SortOrder = blogCategory.SortOrder,
                URL = blogCategory.URL,
                IconCss = blogCategory.IconCss,
                SeoAlias = blogCategory.SeoAlias,
                SeoDescription = blogCategory.SeoDescription,
                SeoKeywords = blogCategory.SeoKeywords,
                SeoPageTitle = blogCategory.SeoPageTitle,
                DateCreated = blogCategory.DateCreated,
                DateModified = blogCategory.DateModified,
                Status = blogCategory.Status,
                IsMain = blogCategory.IsMain,
                Type = blogCategory.Type
            };
        }

        public void Add(BlogCategoryViewModel modeVm)
        {
            CheckSeo(modeVm);

            if (modeVm.ParentId == 0)
                modeVm.ParentId = null;

            var blogCategorydbs = _blogCategoryRepository.FindAll(x => x.ParentId == modeVm.ParentId);

            if (blogCategorydbs.Count() > 0)
                modeVm.SortOrder += blogCategorydbs.Max(x => x.SortOrder);

            var blogCategory = new BlogCategory()
            {
                Name = modeVm.Name,
                Description = modeVm.Description,
                FunctionId = modeVm.FunctionId,
                ParentId = modeVm.ParentId,
                SortOrder = modeVm.SortOrder,
                URL = modeVm.URL,
                IconCss = modeVm.IconCss,
                SeoAlias = modeVm.SeoAlias,
                SeoDescription = modeVm.SeoDescription,
                SeoKeywords = modeVm.SeoKeywords,
                SeoPageTitle = modeVm.SeoPageTitle,
                DateCreated = modeVm.DateCreated,
                DateModified = modeVm.DateModified,
                IsMain = modeVm.IsMain,
                Status = modeVm.Status,
                Type = modeVm.Type
            };

            _blogCategoryRepository.Add(blogCategory);
            _unitOfWork.Commit();

            FormatURL(blogCategory);
            _blogCategoryRepository.Update(blogCategory);
            _unitOfWork.Commit();
        }

        public void CheckSeo(BlogCategoryViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            modeVm.SeoAlias = TextHelper.UrlFriendly(modeVm.Name);
        }

        private void FormatURL(BlogCategory model)
        {
            string url = string.Empty;
            var function = _functionService.GetById(model.FunctionId);
            if (function != null)
                url = function.URL;

            model.URL = url
                .Replace("{alias}", model.SeoAlias)
                .Replace("{id}", model.Id.ToString());
        }

        public void Update(BlogCategoryViewModel modeVm)
        {
            CheckSeo(modeVm);

            var blogCategory = new BlogCategory()
            {
                Id = modeVm.Id,
                Name = modeVm.Name,
                Description = modeVm.Description,
                FunctionId = modeVm.FunctionId,
                ParentId = modeVm.ParentId,
                SortOrder = modeVm.SortOrder,
                URL = modeVm.URL,
                IconCss = modeVm.IconCss,
                SeoAlias = modeVm.SeoAlias,
                SeoDescription = modeVm.SeoDescription,
                SeoKeywords = modeVm.SeoKeywords,
                SeoPageTitle = modeVm.SeoPageTitle,
                DateCreated = modeVm.DateCreated,
                DateModified = modeVm.DateModified,
                Status = modeVm.Status,
                IsMain = modeVm.IsMain,
                Type = modeVm.Type
            };

            FormatURL(blogCategory);

            _blogCategoryRepository.Update(blogCategory);
            _unitOfWork.Commit();
        }

        public void Delete(int id) => _blogCategoryRepository.Remove(id);

        public void Save() => _unitOfWork.Commit();
    }
}
