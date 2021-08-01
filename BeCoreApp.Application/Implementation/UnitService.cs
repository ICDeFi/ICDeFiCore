using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.RealEstate;
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
    public class UnitService : IUnitService
    {
        private IUnitRepository _unitRepository;
        private IUnitOfWork _unitOfWork;

        public UnitService(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<UnitViewModel> GetAllPaging(string startDate, string endDate, string keyword, int typeId, int pageIndex, int pageSize)
        {
            var query = _unitRepository.FindAll();
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
                query = query.Where(x => x.Name.Contains(keyword));

            if (typeId != 0)
                query = query.Where(x => x.TypeId == typeId);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ProjectTo<UnitViewModel>().ToList();

            return new PagedResult<UnitViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<UnitViewModel> GetAll()
        {
            return _unitRepository.FindAll().Where(x => x.Status == Status.Active)
                  .ProjectTo<UnitViewModel>().ToList();
        }

        public List<UnitViewModel> GetAllByTypeId(int typeId)
        {
            return _unitRepository.FindAll().Where(x => x.Status == Status.Active && x.TypeId == typeId)
                  .ProjectTo<UnitViewModel>().ToList();
        }

        public UnitViewModel GetById(int id)
        {
            return Mapper.Map<Unit, UnitViewModel>(_unitRepository.FindById(id));
        }
        public UnitViewModel CheckSeo(UnitViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(UnitViewModel unitVm)
        {
            unitVm.SeoAlias = TextHelper.UrlFriendly(unitVm.Name);

            var unit = Mapper.Map<UnitViewModel, Unit>(CheckSeo(unitVm));

            _unitRepository.Add(unit);
        }

        public void Update(UnitViewModel unitVm)
        {
            unitVm.SeoAlias = TextHelper.UrlFriendly(unitVm.Name);

            var unit = Mapper.Map<UnitViewModel, Unit>(CheckSeo(unitVm));

            _unitRepository.Update(unit);
        }

        public void Delete(int id)
        {
            _unitRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
