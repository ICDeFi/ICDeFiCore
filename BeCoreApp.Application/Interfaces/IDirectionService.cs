using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IDirectionService
    {
        PagedResult<DirectionViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize);

        List<DirectionViewModel> GetAll();

        DirectionViewModel GetById(int id);

        void Add(DirectionViewModel Vm);

        void Update(DirectionViewModel Vm);

        void Delete(int id);

        void Save();
    }
}
