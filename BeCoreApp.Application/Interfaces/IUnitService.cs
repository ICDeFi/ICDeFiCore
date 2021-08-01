using BeCoreApp.Application.ViewModels.RealEstate;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IUnitService
    {
        PagedResult<UnitViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int typeId, int pageIndex, int pageSize);

        List<UnitViewModel> GetAll();

        List<UnitViewModel> GetAllByTypeId(int typeId);

        UnitViewModel GetById(int id);

        void Add(UnitViewModel unitVm);

        void Update(UnitViewModel unitVm);

        void Delete(int id);

        void Save();
    }
}
