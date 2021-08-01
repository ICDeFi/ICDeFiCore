using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Enterprise;
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
    public class FieldService : IFieldService
    {
        private IFieldRepository _fieldRepository;
        private IUnitOfWork _unitOfWork;

        public FieldService(IFieldRepository fieldRepository,
            IUnitOfWork unitOfWork)
        {
            _fieldRepository = fieldRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<FieldViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            var query = _fieldRepository.FindAll();
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
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ProjectTo<FieldViewModel>().ToList();

            return new PagedResult<FieldViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<FieldViewModel> GetAll()
        {
            return _fieldRepository.FindAll().Where(x => x.Status == Status.Active)
                  .ProjectTo<FieldViewModel>().ToList();
        }

        public FieldViewModel GetById(int id)
        {
            return Mapper.Map<Field, FieldViewModel>(_fieldRepository.FindById(id));
        }

        public void Add(FieldViewModel fieldVm)
        {
            fieldVm.SeoAlias = TextHelper.UrlFriendly(fieldVm.Name);
            var field = Mapper.Map<FieldViewModel, Field>(CheckSeo(fieldVm));
            _fieldRepository.Add(field);
        }

        public FieldViewModel CheckSeo(FieldViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Update(FieldViewModel fieldVm)
        {
            fieldVm.SeoAlias = TextHelper.UrlFriendly(fieldVm.Name);
            var field = Mapper.Map<FieldViewModel, Field>(CheckSeo(fieldVm));
            _fieldRepository.Update(field);
        }

        public void Delete(int id)
        {
            _fieldRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
