using BeCoreApp.Application.ViewModels.RealEstate;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IClassifiedCategoryService
    {
        PagedResult<ClassifiedCategoryViewModel> GetAllPaging(string startDate, string endDate, string keyword, int typeId, int pageIndex, int pageSize);

        List<ClassifiedCategoryViewModel> GetAll();

        ClassifiedCategoryViewModel GetById(int id);

        void Add(ClassifiedCategoryViewModel classifiedCategoryVm);

        void Update(ClassifiedCategoryViewModel classifiedCategoryVm);

        void Delete(int id);

        void Save();
    }
}
