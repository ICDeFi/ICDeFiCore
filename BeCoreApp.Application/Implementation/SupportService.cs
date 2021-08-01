using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;

namespace BeCoreApp.Application.Implementation
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupportService(ISupportRepository supportRepository, IUnitOfWork unitOfWork)
        {
            _supportRepository = supportRepository;
            _unitOfWork = unitOfWork;
        }


        public void Add(SupportViewModel supportVm)
        {
            var support = new Support
            {
                Name = supportVm.Name,
                AppUserId = supportVm.AppUserId,
                RequestContent = supportVm.RequestContent,
                Type = SupportType.New,
            };

            _supportRepository.Add(support);
        }

        public void Delete(int id) => _supportRepository.Remove(id);


        public PagedResult<SupportViewModel> GetAllPaging(string keyword, string appUserId, int pageIndex, int pageSize)
        {
            var query = _supportRepository.FindAll(x => x.AppUser);

            if (!string.IsNullOrWhiteSpace(appUserId))
                query = query.Where(x => x.AppUserId == new Guid(appUserId));

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));

            var totalRow = query.Count();
            var data = query.OrderBy(x => x.Type).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new SupportViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    AppUserId = x.AppUserId,
                    AppUserName = x.AppUser.UserName,
                    RequestContent = x.RequestContent,
                    ResponseContent = x.ResponseContent,
                    Type = x.Type
                }).ToList();

            return new PagedResult<SupportViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public SupportViewModel GetById(int id)
        {
            var model = _supportRepository.FindById(id, x => x.AppUser);
            if (model == null)
                return null;

            return new SupportViewModel
            {
                Id = model.Id,
                Name = model.Name,
                AppUserId = model.AppUserId,
                AppUserName = model.AppUser.UserName,
                RequestContent = model.RequestContent,
                ResponseContent = model.ResponseContent,
                Type = model.Type
            };
        }

        public void Save() => _unitOfWork.Commit();

        public void Update(SupportViewModel blogVm)
        {
            var model = _supportRepository.FindById(blogVm.Id);
            model.ResponseContent = blogVm.ResponseContent;
            model.Type = SupportType.Responded;

            _supportRepository.Update(model);
        }
    }
}