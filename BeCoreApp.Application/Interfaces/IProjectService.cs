using BeCoreApp.Application.ViewModels.Project;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IProjectService
    {
        PagedResult<ProjectViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int provinceId, int districtId, int wardId, int enterpriseId,
            int projectCategoryId, int pageIndex, int pageSize);

        List<ProjectViewModel> GetAll();

        List<ProjectViewModel> GetAllByProjectCategoryId(int projectCategoryId);

        ProjectViewModel GetById(int id);

        List<ProjectImageViewModel> GetImages(int projectId);

        void AddImages(int projectId, string[] images);

        List<ProjectLibraryViewModel> GetLibraries(int projectId);

        void AddLibraries(int projectId, string[] images);

        void Add(ProjectViewModel vm);

        void Update(ProjectViewModel vm);

        void Delete(int id);

        void Save();
    }
}
