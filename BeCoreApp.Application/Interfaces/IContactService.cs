using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IContactService
    {
        PagedResult<ContactViewModel> GetAllPaging(string keyword, int page, int pageSize);

        void Add(ContactViewModel contactVm);

        void Update(ContactViewModel contactVm);

        void Delete(string id);

        List<ContactViewModel> GetAll();

        ContactViewModel GetById(string id);

        void SaveChanges();
    }
}
