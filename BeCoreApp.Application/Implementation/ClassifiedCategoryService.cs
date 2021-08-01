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
    public class ClassifiedCategoryService : IClassifiedCategoryService
    {
        private IClassifiedCategoryRepository _classifiedCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ClassifiedCategoryService(
            IClassifiedCategoryRepository classifiedCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _classifiedCategoryRepository = classifiedCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ClassifiedCategoryViewModel> GetAllPaging(
            string startDate, string endDate
            , string keyword, int typeId,
            int pageIndex, int pageSize)
        {
            var query = _classifiedCategoryRepository.FindAll();
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
                .ProjectTo<ClassifiedCategoryViewModel>().ToList();

            return new PagedResult<ClassifiedCategoryViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<ClassifiedCategoryViewModel> GetAll()
        {
            return _classifiedCategoryRepository.FindAll()
                .Where(x => x.Status == Status.Active)
                  .ProjectTo<ClassifiedCategoryViewModel>().ToList();
        }

        public List<ClassifiedCategoryViewModel> GetAllByTypeId(int typeId)
        {
            return _classifiedCategoryRepository.FindAll()
                .Where(x => x.Status == Status.Active && x.TypeId == typeId)
                  .ProjectTo<ClassifiedCategoryViewModel>().ToList();
        }

        public ClassifiedCategoryViewModel GetById(int id)
        {
            return Mapper.Map<ClassifiedCategory, ClassifiedCategoryViewModel>(_classifiedCategoryRepository.FindById(id));
        }

        public void Add(ClassifiedCategoryViewModel classifiedCategoryVm)
        {
            classifiedCategoryVm.SeoAlias = TextHelper.UrlFriendly(classifiedCategoryVm.Name);
            var classifiedCategory = Mapper.Map<ClassifiedCategoryViewModel, ClassifiedCategory>(CheckSeo(classifiedCategoryVm));
            _classifiedCategoryRepository.Add(classifiedCategory);
        }

        public ClassifiedCategoryViewModel CheckSeo(ClassifiedCategoryViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Update(ClassifiedCategoryViewModel classifiedCategoryVm)
        {
            classifiedCategoryVm.SeoAlias = TextHelper.UrlFriendly(classifiedCategoryVm.Name);
            var classifiedCategory = Mapper.Map<ClassifiedCategoryViewModel, ClassifiedCategory>(CheckSeo(classifiedCategoryVm));
            _classifiedCategoryRepository.Update(classifiedCategory);
        }

        public void Delete(int id)
        {
            _classifiedCategoryRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
