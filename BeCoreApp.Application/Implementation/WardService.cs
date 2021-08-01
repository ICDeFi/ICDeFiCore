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
    public class WardService : IWardService
    {
        private IWardRepository _wardRepository;
        private IUnitOfWork _unitOfWork;

        public WardService(IWardRepository wardRepository, IUnitOfWork unitOfWork)
        {
            _wardRepository = wardRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<WardViewModel> GetAllPaging(string startDate, string endDate, string keyword, int provinceId, int districtId, int pageIndex, int pageSize)
        {
            var query = _wardRepository.FindAll();
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

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .ProjectTo<WardViewModel>().ToList();

            return new PagedResult<WardViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<WardViewModel> GetAll()
        {
            return _wardRepository.FindAll()
                .Where(x => x.Status == Status.Active)
            .ProjectTo<WardViewModel>().ToList();
        }

        public WardViewModel GetById(int id)
        {
            return Mapper.Map<Ward, WardViewModel>(_wardRepository.FindById(id));
        }

        public List<WardViewModel> GetAllByDistrictId(int districtId)
        {
            return _wardRepository.FindAll()
                .Where(x => x.Status == Status.Active && x.DistrictId == districtId)
            .ProjectTo<WardViewModel>().ToList();
        }

        public WardViewModel CheckSeo(WardViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(WardViewModel wardVm)
        {
            wardVm.SeoAlias = TextHelper.UrlFriendly(wardVm.Name);
            var ward = Mapper.Map<WardViewModel, Ward>(CheckSeo(wardVm));
            _wardRepository.Add(ward);
        }

        public void Update(WardViewModel wardVm)
        {
            wardVm.SeoAlias = TextHelper.UrlFriendly(wardVm.Name);
            var ward = Mapper.Map<WardViewModel, Ward>(CheckSeo(wardVm));
            _wardRepository.Update(ward);
        }

        public void Delete(int id)
        {
            _wardRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
