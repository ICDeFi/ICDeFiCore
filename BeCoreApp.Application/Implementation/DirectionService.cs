using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class DirectionService : IDirectionService
    {
        private IDirectionRepository _directionRepository;
        private IUnitOfWork _unitOfWork;

        public DirectionService(IDirectionRepository directionRepository,
            IUnitOfWork unitOfWork)
        {
            _directionRepository = directionRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<DirectionViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            var query = _directionRepository.FindAll();
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated >= start);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated <= end);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ProjectTo<DirectionViewModel>().ToList();

            return new PagedResult<DirectionViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<DirectionViewModel> GetAll()
        {
            return _directionRepository.FindAll().Where(x => x.Status == Status.Active)
                  .ProjectTo<DirectionViewModel>().ToList();
        }

        public DirectionViewModel GetById(int id)
        {
            return Mapper.Map<Direction, DirectionViewModel>(_directionRepository.FindById(id));
        }

        public void Add(DirectionViewModel directionVm)
        {
            directionVm.SeoAlias = TextHelper.UrlFriendly(directionVm.Name);
            var direction = Mapper.Map<DirectionViewModel, Direction>(CheckSeo(directionVm));
            _directionRepository.Add(direction);
        }

        public DirectionViewModel CheckSeo(DirectionViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Update(DirectionViewModel directionVm)
        {
            directionVm.SeoAlias = TextHelper.UrlFriendly(directionVm.Name);
            var direction = Mapper.Map<DirectionViewModel, Direction>(CheckSeo(directionVm));
            _directionRepository.Update(direction);
        }

        public void Delete(int id)
        {
            _directionRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
