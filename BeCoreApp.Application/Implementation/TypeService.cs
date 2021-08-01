using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.RealEstate;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BeCoreApp.Utilities.Helpers;

namespace BeCoreApp.Application.Implementation
{
    public class TypeService : ITypeService
    {
        private ITypeRepository _typeRepository;
        private IUnitOfWork _unitOfWork;

        public TypeService(ITypeRepository typeRepository,
            IUnitOfWork unitOfWork)
        {
            _typeRepository = typeRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<TypeViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            var query = _typeRepository.FindAll();
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
                .ProjectTo<TypeViewModel>().ToList();

            return new PagedResult<TypeViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<TypeViewModel> GetAll()
        {
            return _typeRepository.FindAll().Where(x => x.Status == Status.Active)
                  .ProjectTo<TypeViewModel>().ToList();
        }

        public TypeViewModel GetById(int id)
        {
            return Mapper.Map<BeCoreApp.Data.Entities.Type, TypeViewModel>(_typeRepository.FindById(id));
        }

        public TypeViewModel CheckSeo(TypeViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(TypeViewModel typeVm)
        {
            typeVm.SeoAlias = TextHelper.UrlFriendly(typeVm.Name);
            var type = Mapper.Map<TypeViewModel, BeCoreApp.Data.Entities.Type>(CheckSeo(typeVm));
            _typeRepository.Add(type);
        }

        public void Update(TypeViewModel typeVm)
        {
            typeVm.SeoAlias = TextHelper.UrlFriendly(typeVm.Name);
            var type = Mapper.Map<TypeViewModel, BeCoreApp.Data.Entities.Type>(CheckSeo(typeVm));
            _typeRepository.Update(type);
        }

        public void Delete(int id)
        {
            _typeRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
