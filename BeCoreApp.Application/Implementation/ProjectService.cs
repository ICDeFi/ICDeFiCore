using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
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

namespace BeCoreApp.Application.Implementation
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepository;
        private IProjectImageRepository _projectImageRepository;
        private IProjectLibraryRepository _projectLibraryRepository;
        private IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepository,
            IProjectImageRepository projectImageRepository,
            IProjectLibraryRepository projectLibraryRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _projectImageRepository = projectImageRepository;
            _projectLibraryRepository = projectLibraryRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ProjectViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int provinceId, int districtId, int wardId, int enterpriseId, int projectCategoryId, int pageIndex, int pageSize)
        {
            var query = _projectRepository.FindAll();
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

            if (enterpriseId != 0)
                query = query.Where(x => x.EnterpriseId == enterpriseId);

            if (projectCategoryId != 0)
                query = query.Where(x => x.ProjectCategoryId == projectCategoryId);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .ProjectTo<ProjectViewModel>().ToList();

            return new PagedResult<ProjectViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<ProjectViewModel> GetAll()
        {
            return _projectRepository.FindAll().Where(x => x.Status == Status.Active)
                .ProjectTo<ProjectViewModel>().ToList();
        }
        public List<ProjectViewModel> GetAllByProjectCategoryId(int projectCategoryId)
        {
            return _projectRepository.FindAll().Where(x => x.Status == Status.Active
                        && x.ProjectCategoryId == projectCategoryId).ProjectTo<ProjectViewModel>().ToList();
        }
        public ProjectViewModel GetById(int id)
        {
            return Mapper.Map<Project, ProjectViewModel>(_projectRepository.FindById(id));
        }

        public List<ProjectImageViewModel> GetImages(int projectId)
        {
            return _projectImageRepository.FindAll(x => x.ProjectId == projectId)
                .ProjectTo<ProjectImageViewModel>().ToList();
        }

        public void AddImages(int projectId, string[] images)
        {
            _projectImageRepository.RemoveMultiple(_projectImageRepository.FindAll(x => x.ProjectId == projectId).ToList());
            foreach (var image in images)
            {
                _projectImageRepository.Add(new ProjectImage()
                {
                    Path = image,
                    ProjectId = projectId,
                    Caption = string.Empty
                });
            }
        }

        public List<ProjectLibraryViewModel> GetLibraries(int projectId)
        {
            return _projectLibraryRepository.FindAll(x => x.ProjectId == projectId)
                .ProjectTo<ProjectLibraryViewModel>().ToList();
        }

        public void AddLibraries(int projectId, string[] images)
        {
            _projectLibraryRepository.RemoveMultiple(_projectLibraryRepository.FindAll(x => x.ProjectId == projectId).ToList());
            foreach (var image in images)
            {
                _projectLibraryRepository.Add(new ProjectLibrary()
                {
                    Path = image,
                    ProjectId = projectId,
                    Caption = string.Empty
                });
            }
        }

        public ProjectViewModel CheckSeo(ProjectViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Add(ProjectViewModel vm)
        {
            vm.SeoAlias = TextHelper.UrlFriendly(vm.Name);
            var project = Mapper.Map<ProjectViewModel, Project>(CheckSeo(vm));
            _projectRepository.Add(project);
        }

        public void Update(ProjectViewModel vm)
        {
            vm.SeoAlias = TextHelper.UrlFriendly(vm.Name);
            var project = Mapper.Map<ProjectViewModel, Project>(CheckSeo(vm));
            _projectRepository.Update(project);
        }

        public void Delete(int id)
        {
            _projectRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
