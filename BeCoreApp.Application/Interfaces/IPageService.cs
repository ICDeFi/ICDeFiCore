using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IPageService : IDisposable
    {
        PagedResult<PageViewModel> GetAllPaging(string keyword, int page, int pageSize);

        void Add(PageViewModel pageVm);

        void Update(PageViewModel pageVm);

        void Delete(int id);

        List<PageViewModel> GetAll();

        PageViewModel GetByAlias(string alias);

        PageViewModel GetById(int id);

        void SaveChanges();
    }
}
