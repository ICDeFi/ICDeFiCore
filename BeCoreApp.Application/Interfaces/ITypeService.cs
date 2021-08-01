using BeCoreApp.Application.ViewModels.RealEstate;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface ITypeService
    {
        PagedResult<TypeViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        List<TypeViewModel> GetAll();

        TypeViewModel GetById(int id);

        void Add(TypeViewModel typeVm);

        void Update(TypeViewModel typeVm);

        void Delete(int id);

        void Save();
    }
}
