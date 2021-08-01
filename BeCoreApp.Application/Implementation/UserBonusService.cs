using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeCoreApp.Application.Implementation
{
    public class UserBonusService : IUserBonusService
    {
        private IUserBonusRepository _userBonusRepository;
        private IUnitOfWork _unitOfWork;

        public UserBonusService(IUserBonusRepository userBonusRepository,
            IUnitOfWork unitOfWork)
        {
            _userBonusRepository = userBonusRepository;
            _unitOfWork = unitOfWork;
        }


        public List<UserBonusViewModel> GetAll()
        {
            return _userBonusRepository.FindAll()
                .Select(x => new UserBonusViewModel
                {
                    Id = x.Id,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    LevelStrategy = x.LevelStrategy,
                    LevelStrategyName = x.LevelStrategy.GetDescription(),
                    MoneyReceive = x.MoneyReceive,
                    RecruitingBonus = x.RecruitingBonus,
                    RewardPoint = x.RewardPoint,
                    Status = x.Status,
                    UplineFund = x.UplineFund
                }).ToList();
        }

        public UserBonusViewModel GetByLevelStrategy(UserLevelStrategyStatus levelStrategy)
        {
            var model = _userBonusRepository.FindSingle(x => x.LevelStrategy == levelStrategy);

            if (model == null)
                return null;

            return new UserBonusViewModel()
            {
                Id = model.Id,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
                LevelStrategy = model.LevelStrategy,
                LevelStrategyName = model.LevelStrategy.GetDescription(),
                MoneyReceive = model.MoneyReceive,
                RecruitingBonus = model.RecruitingBonus,
                RewardPoint = model.RewardPoint,
                Status = model.Status,
                UplineFund = model.UplineFund
            };
        }

        public UserBonusViewModel GetById(int id)
        {
            var model = _userBonusRepository.FindById(id);

            if (model == null)
                return null;

            return new UserBonusViewModel()
            {
                Id = model.Id,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
                LevelStrategy = model.LevelStrategy,
                LevelStrategyName = model.LevelStrategy.GetDescription(),
                MoneyReceive = model.MoneyReceive,
                RecruitingBonus = model.RecruitingBonus,
                RewardPoint = model.RewardPoint,
                Status = model.Status,
                UplineFund = model.UplineFund
            };
        }

        public void Update(UserBonusViewModel model)
        {
            var appUserBonus = _userBonusRepository.FindById(model.Id);
            if (appUserBonus != null)
            {
                appUserBonus.UplineFund = model.UplineFund;
                appUserBonus.MoneyReceive = model.MoneyReceive;
                appUserBonus.DateModified = DateTime.Now;
                appUserBonus.RecruitingBonus = model.RecruitingBonus;
                appUserBonus.RewardPoint = model.RewardPoint;

                _userBonusRepository.Update(appUserBonus);
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
