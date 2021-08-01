using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface ISlideService
    {
        PagedResult<SlideViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize);

        SlideViewModel Add(SlideViewModel model);

        void Update(SlideViewModel model);

        void Delete(int id);

        List<SlideViewModel> GetLatests(int top);

        List<SlideViewModel> GetHots(int top);

        SlideViewModel GetById(int id);

        void Save();
    }
}
