using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Extensions;
using BeCoreApp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class AppUserLuckyRoomService : IAppUserLuckyRoomService
    {
        private IAppUserLuckyRoomRepository _appUserLuckyRoomRepository;
        private IUnitOfWork _unitOfWork;

        public AppUserLuckyRoomService(
            IAppUserLuckyRoomRepository appUserLuckyRoomRepository,
            IUnitOfWork unitOfWork
            )
        {
            _appUserLuckyRoomRepository = appUserLuckyRoomRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<AppUserLuckyRoomViewModel> GetAllPaging(int pageIndex, int pageSize)
        {
            var query = _appUserLuckyRoomRepository.FindAll(x => x.AppUser, p => p.LuckyRoom);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new AppUserLuckyRoomViewModel()
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    AppUserName = x.AppUser.UserName,
                    LuckyRoomName = $" {x.LuckyRoom.Type.GetDescription()}-{x.LuckyRoomId}",
                    LuckyRoomId = x.LuckyRoomId,
                    AmountBet = x.AmountBet,
                    AmountReceive = x.AmountReceive,
                    Status = x.Status,
                    StatusName = x.Status.GetDescription(),
                    DateCreated = x.DateCreated,
                }).ToList();

            return new PagedResult<AppUserLuckyRoomViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<AppUserLuckyRoomViewModel> GetAllByUserId(string userId)
        {
            var query = _appUserLuckyRoomRepository.FindAll(x => x.AppUser, p => p.LuckyRoom);

            query = query.OrderByDescending(x => x.Id)
                .Where(xp => xp.AppUserId == new Guid(userId));

            var data = query.Select(x => new AppUserLuckyRoomViewModel()
            {
                Id = x.Id,
                AmountBet = x.AmountBet,
                AmountReceive = x.AmountReceive,
                AppUserId = x.AppUserId,
                AppUserName = x.AppUser.UserName,
                LuckyRoomName = $" {x.LuckyRoom.Type.GetDescription()}-{x.LuckyRoomId}",
                LuckyRoomId = x.LuckyRoomId,
                Status = x.Status,
                StatusName = x.Status.GetDescription(),
                DateCreated = x.DateCreated,
            }).ToList();

            return data;
        }

        public List<AppUserLuckyRoomViewModel> GetDetailByLuckyRoomId(int luckyRoomId)
        {
            var query = _appUserLuckyRoomRepository
                .FindAll(p => p.LuckyRoomId == luckyRoomId, x => x.AppUser, p => p.LuckyRoom);

            var data = query.OrderBy(x=>x.Status).Select(x => new AppUserLuckyRoomViewModel()
            {
                Id = x.Id,
                AmountBet = x.AmountBet,
                AmountReceive = x.AmountReceive,
                AppUserId = x.AppUserId,
                AppUserName = x.AppUser.UserName,
                LuckyRoomName = $" {x.LuckyRoom.Type.GetDescription()}-{x.LuckyRoomId}",
                LuckyRoomId = x.LuckyRoomId,
                Status = x.Status,
                StatusName = x.Status.GetDescription(),
                DateCreated = x.DateCreated,
            }).ToList();

            return data;
        }

        public AppUserLuckyRoom GetByLuckyRoomIdAndUserId(Guid userId, int luckyRoomId)
        {
            var appUserLuckyRoom = _appUserLuckyRoomRepository.FindAll()
                .FirstOrDefault(x => x.LuckyRoomId == luckyRoomId && x.AppUserId == userId);
            if (appUserLuckyRoom == null)
                return null;

            return appUserLuckyRoom;
        }

        public List<AppUserLuckyRoomViewModel> GetAllByLuckyRoomId(int luckyRoomId)
        {
            var query = _appUserLuckyRoomRepository.FindAll(x => x.LuckyRoomId == luckyRoomId);
            var data = query.OrderBy(x => x.Id)
                .Select(x => new AppUserLuckyRoomViewModel()
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    LuckyRoomId = x.LuckyRoomId,
                    AmountBet = x.AmountBet,
                    AmountReceive = x.AmountReceive,
                    Status = x.Status,
                    DateCreated = x.DateCreated
                }).ToList();

            return data;
        }


        public AppUserLuckyRoom Add(AppUserLuckyRoomViewModel x)
        {
            var appUserLuckyRoom = new AppUserLuckyRoom()
            {
                AppUserId = x.AppUserId,
                LuckyRoomId = x.LuckyRoomId,
                AmountBet = x.AmountBet,
                AmountReceive = x.AmountReceive,
                Status = x.Status,
                DateCreated = DateTime.Now
            };

            _appUserLuckyRoomRepository.Add(appUserLuckyRoom);

            return appUserLuckyRoom;
        }

        public AppUserLuckyRoom Update(AppUserLuckyRoomViewModel x)
        {
            var appUserLuckyRoom = _appUserLuckyRoomRepository.FindById(x.Id);

            appUserLuckyRoom.Id = x.Id;
            appUserLuckyRoom.AppUserId = x.AppUserId;
            appUserLuckyRoom.LuckyRoomId = x.LuckyRoomId;
            appUserLuckyRoom.AmountBet = x.AmountBet;
            appUserLuckyRoom.AmountReceive = x.AmountReceive;
            appUserLuckyRoom.Status = x.Status;
            appUserLuckyRoom.DateCreated = DateTime.Now;

            _appUserLuckyRoomRepository.Update(appUserLuckyRoom);

            return appUserLuckyRoom;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
