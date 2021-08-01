using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IEnterpriseService
    {
        PagedResult<EnterpriseViewModel> GetAllPaging(string startDate, string endDate, string keyword, int fieldId,
            int provinceId, int districtId, int wardId, int pageIndex, int pageSize);
        List<EnterpriseViewModel> GetAll();

        EnterpriseViewModel GetById(int id);

        void Add(EnterpriseViewModel enterpriseVm);

        void Update(EnterpriseViewModel enterpriseVm);

        void Delete(int id);

        void Save();
    }
}
