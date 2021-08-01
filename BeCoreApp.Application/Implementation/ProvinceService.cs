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
    public class ProvinceService : IProvinceService
    {
        private IProvinceRepository _provinceRepository;
        private IUnitOfWork _unitOfWork;

        public ProvinceService(IProvinceRepository provinceRepository,
            IUnitOfWork unitOfWork)
        {
            _provinceRepository = provinceRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ProvinceViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            var query = _provinceRepository.FindAll();
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
                .ProjectTo<ProvinceViewModel>().ToList();

            return new PagedResult<ProvinceViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<ProvinceViewModel> GetAll()
        {
            return _provinceRepository.FindAll()
                .Where(x => x.Status == Status.Active)
            .ProjectTo<ProvinceViewModel>().ToList();
        }

        public ProvinceViewModel GetById(int id)
        {
            return Mapper.Map<Province, ProvinceViewModel>(_provinceRepository.FindById(id));
        }

        public ProvinceViewModel CheckSeo(ProvinceViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(ProvinceViewModel provinceVm)
        {
            provinceVm.SeoAlias = TextHelper.UrlFriendly(provinceVm.Name);
            var province = Mapper.Map<ProvinceViewModel, Province>(CheckSeo(provinceVm));
            _provinceRepository.Add(province);
        }

        public void Update(ProvinceViewModel provinceVm)
        {
            provinceVm.SeoAlias = TextHelper.UrlFriendly(provinceVm.Name);
            var province = Mapper.Map<ProvinceViewModel, Province>(CheckSeo(provinceVm));
            _provinceRepository.Update(province);
        }

        public void Delete(int id)
        {
            _provinceRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
