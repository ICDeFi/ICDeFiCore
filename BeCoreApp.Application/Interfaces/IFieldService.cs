using BeCoreApp.Application.ViewModels.Enterprise;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IFieldService
    {
        PagedResult<FieldViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize);

        List<FieldViewModel> GetAll();

        FieldViewModel GetById(int id);

        void Add(FieldViewModel fieldVm);

        void Update(FieldViewModel fieldVm);

        void Delete(int id);

        void Save();
    }
}
