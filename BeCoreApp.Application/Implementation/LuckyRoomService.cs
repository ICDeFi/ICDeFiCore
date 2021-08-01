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
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class LuckyRoomService : ILuckyRoomService
    {
        private ILuckyRoomRepository _luckyRoomRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _userManager;
        public LuckyRoomService(
            UserManager<AppUser> userManager,
            ILuckyRoomRepository luckyRoomRepository,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _luckyRoomRepository = luckyRoomRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<LuckyRoomViewModel> GetAllPaging(string keyword, LuckyRoomType type, int pageIndex, int pageSize)
        {
            var query = _luckyRoomRepository.FindAll(lr => lr.Type == type, x => x.AppUserLuckyRooms);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Status)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new LuckyRoomViewModel()
                {
                    Id = x.Id,
                    Type = x.Type,
                    TypeName = x.Type.GetDescription(),
                    Status = x.Status,
                    StatusName = x.Status.GetDescription(),
                    DateCreated = x.DateCreated,
                    TotalNumberofGamers = x.AppUserLuckyRooms.Count()
                }).ToList();

            return new PagedResult<LuckyRoomViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public LuckyRoomViewModel GetById(int id)
        {
            var model = _luckyRoomRepository.FindById(id, x => x.AppUserLuckyRooms);
            if (model == null)
                return null;

            var appUserLuckyRooms = model.AppUserLuckyRooms;

            return new LuckyRoomViewModel
            {
                Id = model.Id,
                DateCreated = model.DateCreated,
                Status = model.Status,
                Type = model.Type,
                TotalNumberofGamers = model.AppUserLuckyRooms.Count(),
            };
        }

        public LuckyRoom Add(LuckyRoomViewModel x)
        {
            var luckyRoom = new LuckyRoom()
            {
                Id = x.Id,
                Type = x.Type,
                Status = x.Status,
                DateCreated = DateTime.Now,
            };

            _luckyRoomRepository.Add(luckyRoom);

            return luckyRoom;
        }

        public LuckyRoom Update(LuckyRoomViewModel x)
        {
            var luckyRoom = _luckyRoomRepository.FindById(x.Id);

            luckyRoom.Id = x.Id;
            luckyRoom.Type = x.Type;
            luckyRoom.Status = x.Status;
            luckyRoom.DateCreated = DateTime.Now;

            _luckyRoomRepository.Update(luckyRoom);

            return luckyRoom;
        }

        public async Task<LuckyRoomViewModel> GetNewRoomByType(LuckyRoomType type)
        {
            var luckyRoom = _luckyRoomRepository
                .FindAll(x => x.Status == LuckyRoomStatus.New && x.Type == type)
                .FirstOrDefault();

            if (luckyRoom == null)
            {
                _luckyRoomRepository.Add(new LuckyRoom()
                {
                    Type = type,
                    Status = LuckyRoomStatus.New,
                    DateCreated = DateTime.Now,
                });
                _unitOfWork.Commit();
            }

            var query = _luckyRoomRepository
                .FindAll(x => x.Status == LuckyRoomStatus.New && x.Type == type, al => al.AppUserLuckyRooms)
                .FirstOrDefault();

            var model = new LuckyRoomViewModel();

            var appUserLuckyRooms = query.AppUserLuckyRooms.OrderByDescending(o => o.Id);

            var lastGamerName = "N/A";

            var totalNumberofGamers = appUserLuckyRooms.Count();
            if (totalNumberofGamers > 0)
            {
                var appUserLuckyRoom = appUserLuckyRooms.FirstOrDefault();
                var appUser = await _userManager.FindByIdAsync(appUserLuckyRoom.AppUserId.ToString());
                lastGamerName = appUser.UserName;
            }

            string previousWinner = "N/A";

            var previousLuckyRoom = _luckyRoomRepository
                .FindAll(lr => lr.Type == query.Type && lr.Status == LuckyRoomStatus.Finish, al => al.AppUserLuckyRooms)
                .OrderByDescending(ob => ob.Id).FirstOrDefault();

            if (previousLuckyRoom != null)
            {
                var previousAppUserLuckyRoom = previousLuckyRoom.AppUserLuckyRooms
                    .FirstOrDefault(alr => alr.Status == AppUserLuckyRoomStatus.Win);

                if (previousAppUserLuckyRoom != null)
                {
                    var userWinner = await _userManager.FindByIdAsync(previousAppUserLuckyRoom.AppUserId.ToString());
                    if (userWinner != null)
                    {
                        previousWinner = userWinner.UserName;
                    }
                }
            }

            model = new LuckyRoomViewModel
            {
                Id = query.Id,
                DateCreated = query.DateCreated,
                Status = query.Status,
                StatusName = query.Status.GetDescription(),
                Type = query.Type,
                TypeName = query.Type.GetDescription(),
                TotalNumberofGamers = totalNumberofGamers,
                LastGamerName = lastGamerName,
                PreviousWinner = previousWinner,

            };

            return model;
        }

        public LuckyRoomViewModel GetNewRoom(LuckyRoomType type)
        {
            var model = new LuckyRoomViewModel();

            var luckyRoom = _luckyRoomRepository
                .FindAll(x => x.Status == LuckyRoomStatus.New && x.Type == type)
                .FirstOrDefault();

            if (luckyRoom == null)
                return null;

            model = new LuckyRoomViewModel
            {
                Id = luckyRoom.Id,
                DateCreated = luckyRoom.DateCreated,
                Status = luckyRoom.Status,
                Type = luckyRoom.Type
            };

            return model;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
