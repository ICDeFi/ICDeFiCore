using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.Product;
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
    public class StreetService : IStreetService
    {
        private IStreetRepository _streetRepository;
        private IUnitOfWork _unitOfWork;

        public StreetService(IStreetRepository streetRepository,
            IUnitOfWork unitOfWork)
        {
            _streetRepository = streetRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<StreetViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int provinceId, int districtId, int wardId, int pageIndex, int pageSize)
        {
            var query = _streetRepository.FindAll();
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

            if (provinceId != 0)
                query = query.Where(x => x.ProvinceId == provinceId);

            if (districtId != 0)
                query = query.Where(x => x.DistrictId == districtId);

            if (wardId != 0)
                query = query.Where(x => x.WardId == wardId);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .ProjectTo<StreetViewModel>().ToList();

            return new PagedResult<StreetViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<StreetViewModel> GetAll()
        {
            return _streetRepository.FindAll()
                .Where(x => x.Status == Status.Active)
                .ProjectTo<StreetViewModel>().ToList();
        }

        public List<StreetViewModel> GetAllByWardId(int wardId)
        {
            return _streetRepository.FindAll()
                .Where(x => x.Status == Status.Active && x.WardId == wardId)
                .ProjectTo<StreetViewModel>().ToList();
        }

        public StreetViewModel GetById(int id)
        {
            return Mapper.Map<Street, StreetViewModel>(_streetRepository.FindById(id));
        }

        public StreetViewModel CheckSeo(StreetViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(StreetViewModel streetVm)
        {
            streetVm.SeoAlias = TextHelper.UrlFriendly(streetVm.Name);
            var street = Mapper.Map<StreetViewModel, Street>(CheckSeo(streetVm));
            _streetRepository.Add(street);
        }

        public void Update(StreetViewModel streetVm)
        {
            streetVm.SeoAlias = TextHelper.UrlFriendly(streetVm.Name);
            var street = Mapper.Map<StreetViewModel, Street>(CheckSeo(streetVm));
            _streetRepository.Update(street);
        }

        public void Delete(int id)
        {
            _streetRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
