using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.Product;
using BeCoreApp.Application.ViewModels.Project;
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
    public class ProjectCategoryService : IProjectCategoryService
    {
        private IProjectCategoryRepository _projectCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProjectCategoryService(IProjectCategoryRepository projectCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _projectCategoryRepository = projectCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ProjectCategoryViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            var query = _projectCategoryRepository.FindAll();
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
                .ProjectTo<ProjectCategoryViewModel>().ToList();

            return new PagedResult<ProjectCategoryViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<ProjectCategoryViewModel> GetAll()
        {
            return _projectCategoryRepository.FindAll()
                .Where(x => x.Status == Status.Active)
            .ProjectTo<ProjectCategoryViewModel>().ToList();
        }

        public ProjectCategoryViewModel GetById(int id)
        {
            return Mapper.Map<ProjectCategory, ProjectCategoryViewModel>(_projectCategoryRepository.FindById(id));
        }

        public ProjectCategoryViewModel CheckSeo(ProjectCategoryViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(ProjectCategoryViewModel projectCategoryVm)
        {
            projectCategoryVm.SeoAlias = TextHelper.UrlFriendly(projectCategoryVm.Name);
            var projectCategory = Mapper.Map<ProjectCategoryViewModel, ProjectCategory>(CheckSeo(projectCategoryVm));
            _projectCategoryRepository.Add(projectCategory);
        }

        public void Update(ProjectCategoryViewModel projectCategoryVm)
        {
            projectCategoryVm.SeoAlias = TextHelper.UrlFriendly(projectCategoryVm.Name);
            var projectCategory = Mapper.Map<ProjectCategoryViewModel, ProjectCategory>(CheckSeo(projectCategoryVm));
            _projectCategoryRepository.Update(projectCategory);
        }

        public void Delete(int id)
        {
            _projectCategoryRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
