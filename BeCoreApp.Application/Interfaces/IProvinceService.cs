using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IProvinceService
    {
        PagedResult<ProvinceViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        List<ProvinceViewModel> GetAll();

        ProvinceViewModel GetById(int id);

        void Add(ProvinceViewModel provinceVm);

        void Update(ProvinceViewModel provinceVm);

        void Delete(int id);

        void Save();
    }
}
