using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IStreetService
    {
        PagedResult<StreetViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int provinceId, int districtId, int wardId, int pageIndex, int pageSize);

        List<StreetViewModel> GetAll();

        List<StreetViewModel> GetAllByWardId(int wardId);

        StreetViewModel GetById(int id);

        void Add(StreetViewModel streetVm);

        void Update(StreetViewModel streetVm);

        void Delete(int id);

        void Save();
    }
}
