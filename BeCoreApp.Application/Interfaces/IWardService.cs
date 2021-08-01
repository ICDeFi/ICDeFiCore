using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IWardService
    {
        PagedResult<WardViewModel> GetAllPaging(string startDate, string endDate, string keyword, int provinceId, int districtId, int pageIndex, int pageSize);

        List<WardViewModel> GetAll();

        WardViewModel GetById(int id);

        List<WardViewModel> GetAllByDistrictId(int districtId);

        void Add(WardViewModel wardVm);

        void Update(WardViewModel wardVm);

        void Delete(int id);

        void Save();
    }
}
