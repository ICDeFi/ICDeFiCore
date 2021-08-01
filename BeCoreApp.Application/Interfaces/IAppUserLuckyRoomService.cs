using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeCoreApp.Application.Interfaces
{
    public interface IAppUserLuckyRoomService
    {
        PagedResult<AppUserLuckyRoomViewModel> GetAllPaging(int pageIndex, int pageSize);

        AppUserLuckyRoom GetByLuckyRoomIdAndUserId(Guid userId, int luckyRoomId);

        List<AppUserLuckyRoomViewModel> GetAllByUserId(string userId);

        List<AppUserLuckyRoomViewModel> GetDetailByLuckyRoomId(int luckyRoomId);

        List<AppUserLuckyRoomViewModel> GetAllByLuckyRoomId(int luckyRoomId);

        AppUserLuckyRoom Add(AppUserLuckyRoomViewModel Model);

        AppUserLuckyRoom Update(AppUserLuckyRoomViewModel Model);

        void Save();
    }
}
