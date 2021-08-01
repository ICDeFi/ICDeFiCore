using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Interfaces
{
    public interface ILuckyRoomService
    {
        PagedResult<LuckyRoomViewModel> GetAllPaging(string keyword, LuckyRoomType type, int pageIndex, int pageSize);

        Task<LuckyRoomViewModel> GetNewRoomByType(LuckyRoomType type);

        LuckyRoomViewModel GetNewRoom(LuckyRoomType type);

        LuckyRoomViewModel GetById(int id);

        LuckyRoom Add(LuckyRoomViewModel Model);

        LuckyRoom Update(LuckyRoomViewModel Model);

        void Save();
    }
}
