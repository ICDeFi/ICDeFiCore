using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Dtos;
using System.Collections.Generic;

namespace BeCoreApp.Application.Interfaces
{
    public interface IUserBonusService
    {
        List<UserBonusViewModel> GetAll();

        UserBonusViewModel GetByLevelStrategy(UserLevelStrategyStatus levelStrategy);

        UserBonusViewModel GetById(int id);

        void Update(UserBonusViewModel userBonusVm);

        void Save();
    }
}
