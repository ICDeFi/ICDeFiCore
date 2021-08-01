using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface ISupportService
    {
        PagedResult<SupportViewModel> GetAllPaging(string keyword,string appUserId, int pageIndex, int pageSize);
        SupportViewModel GetById(int id);
        void Add(SupportViewModel support);
        void Update(SupportViewModel support);
        void Delete(int id);
        void Save();
    }
}
