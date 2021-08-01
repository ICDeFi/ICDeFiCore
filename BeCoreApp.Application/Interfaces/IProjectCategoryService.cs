using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IProjectCategoryService
    {
        PagedResult<ProjectCategoryViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize);

        List<ProjectCategoryViewModel> GetAll();

        ProjectCategoryViewModel GetById(int id);

        void Add(ProjectCategoryViewModel projectCategoryVm);

        void Update(ProjectCategoryViewModel projectCategoryVm);

        void Delete(int id);

        void Save();
    }
}
