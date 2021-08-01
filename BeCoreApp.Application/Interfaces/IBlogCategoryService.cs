using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IBlogCategoryService
    {
        List<int> GetBlogCategoryIdsById(int id, MenuFrontEndType type);
        string GetHomeSidebarMenuStringByType(MenuFrontEndType type);
        string GetMobileMenuStringByType(MenuFrontEndType type);

        string GetMobileMainMenuStringByType(MenuFrontEndType type);

        string GetMobileMainMenuString();

        string GetMainMenuString();
        List<BlogCategoryViewModel> GetMainItems();
        List<BlogCategoryViewModel> SideBarCategoryByType(MenuFrontEndType type);

        List<BlogCategoryTreeViewModel> GetTreeAll();

        void UpdateParentId(int id, int? newParentId);

        BlogCategoryViewModel GetById(int id);

        void Add(BlogCategoryViewModel blogCategoryVm);

        void Update(BlogCategoryViewModel blogCategoryVm);

        void Delete(int id);

        void Save();
    }
}
