using BeCoreApp.Application.ViewModels.Enterprise;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IEnterpriseFieldService
    {
        List<EnterpriseFieldViewModel> GetAllByFieldId(int fieldId);
        List<EnterpriseFieldViewModel> GetAllByEnterpriseId(int enterpriseId);
        EnterpriseFieldViewModel GetById(int id);
        void Add(EnterpriseFieldViewModel enterpriseFieldVm);
        void Update(EnterpriseFieldViewModel enterpriseFieldVm);
        void Delete(int id);
        void Save();
    }
}
