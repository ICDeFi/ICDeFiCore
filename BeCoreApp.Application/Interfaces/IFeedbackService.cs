using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IFeedbackService
    {
        PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize);

        void Add(FeedbackViewModel feedbackVm);

        void UpdateType(int id, string modifiedBy);

        void Delete(int id);

        FeedbackViewModel GetById(int id);

        void SaveChanges();
    }
}
