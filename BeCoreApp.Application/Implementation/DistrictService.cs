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
    public class DistrictService : IDistrictService
    {
        private IRepository<District, int> _districtRepository;
        private IUnitOfWork _unitOfWork;

        public DistrictService(IRepository<District, int> districtRepository,
            IUnitOfWork unitOfWork)
        {
            _districtRepository = districtRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<DistrictViewModel> GetAllPaging(string startDate, string endDate, string keyword, int provinceId, int pageIndex, int pageSize)
        {
            var query = _districtRepository.FindAll();
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
            if (provinceId != 0)
            {
                query = query.Where(x => x.ProvinceId == provinceId);
            }
            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ProjectTo<DistrictViewModel>().ToList();

            return new PagedResult<DistrictViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<DistrictViewModel> GetAll()
        {
            return _districtRepository.FindAll()
                .Where(x => x.Status == Status.Active)
            .ProjectTo<DistrictViewModel>().ToList();
        }
        public List<DistrictViewModel> GetAllByProvinceId(int provinceId)
        {
            return _districtRepository.FindAll()
                .Where(x => x.Status == Status.Active && x.ProvinceId == provinceId)
            .ProjectTo<DistrictViewModel>().ToList();
        }
        public DistrictViewModel GetById(int id)
        {
            return Mapper.Map<District, DistrictViewModel>(_districtRepository.FindById(id));
        }
        public DistrictViewModel CheckSeo(DistrictViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(DistrictViewModel districtVm)
        {
            districtVm.SeoAlias = TextHelper.UrlFriendly(districtVm.Name);
            var district = Mapper.Map<DistrictViewModel, District>(CheckSeo(districtVm));
            _districtRepository.Add(district);
        }

        public void Update(DistrictViewModel districtVm)
        {
            districtVm.SeoAlias = TextHelper.UrlFriendly(districtVm.Name);
            var district = Mapper.Map<DistrictViewModel, District>(CheckSeo(districtVm));
            _districtRepository.Update(district);
        }

        public void Delete(int id)
        {
            _districtRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
