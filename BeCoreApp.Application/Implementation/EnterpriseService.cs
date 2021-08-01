using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Enterprise;
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
    public class EnterpriseService : IEnterpriseService
    {
        private IEnterpriseRepository _enterpriseRepository;
        private IEnterpriseFieldService _enterpriseFieldService;
        private IUnitOfWork _unitOfWork;

        public EnterpriseService(IEnterpriseRepository enterpriseRepository,
            IEnterpriseFieldService enterpriseFieldService,
            IUnitOfWork unitOfWork)
        {
            _enterpriseRepository = enterpriseRepository;
            _enterpriseFieldService = enterpriseFieldService;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<EnterpriseViewModel> GetAllPaging(string startDate, string endDate, string keyword, int fieldId,
            int provinceId, int districtId, int wardId, int pageIndex, int pageSize)
        {
            var query = _enterpriseRepository.FindAll();
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

            if (fieldId != 0)
                query = query.Where(x => x.EnterpriseFields.Any(ef => ef.FieldId == fieldId));

            if (provinceId != 0)
                query = query.Where(x => x.ProvinceId == provinceId);

            if (districtId != 0)
                query = query.Where(x => x.DistrictId == districtId);

            if (wardId != 0)
                query = query.Where(x => x.WardId == wardId);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ProjectTo<EnterpriseViewModel>().ToList();

            return new PagedResult<EnterpriseViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public List<EnterpriseViewModel> GetAll()
        {
            return _enterpriseRepository.FindAll()
                .Where(x => x.Status == Status.Active)
            .ProjectTo<EnterpriseViewModel>().ToList();
        }
        public EnterpriseViewModel GetById(int id)
        {
            return Mapper.Map<Enterprise, EnterpriseViewModel>(_enterpriseRepository.FindById(id));
        }

        public void Add(EnterpriseViewModel enterpriseVm)
        {
            enterpriseVm.SeoAlias = TextHelper.UrlFriendly(enterpriseVm.Name);

            var enterprise = Mapper.Map<EnterpriseViewModel, Enterprise>(CheckSeo(enterpriseVm));
            _enterpriseRepository.Add(enterprise);

            var newEnterpriseFields = enterpriseVm.EnterpriseFieldVMs.ToList();
            for (int i = 0; i < newEnterpriseFields.Count; i++)
            {
                _enterpriseFieldService.Add(new EnterpriseFieldViewModel()
                {
                    FieldId = newEnterpriseFields[i].FieldId,
                    EnterpriseId = enterprise.Id,
                    Status = Status.Active,
                });
            }
        }

        public EnterpriseViewModel CheckSeo(EnterpriseViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Update(EnterpriseViewModel enterpriseVm)
        {
            var enterpriseFields = _enterpriseFieldService.GetAllByEnterpriseId(enterpriseVm.Id);
            for (int i = 0; i < enterpriseFields.Count; i++)
            {
                _enterpriseFieldService.Delete(enterpriseFields[i].Id);
            }

            enterpriseVm.SeoAlias = TextHelper.UrlFriendly(enterpriseVm.Name);
            var enterprise = Mapper.Map<EnterpriseViewModel, Enterprise>(CheckSeo(enterpriseVm));
            _enterpriseRepository.Update(enterprise);

            var newEnterpriseFields = enterpriseVm.EnterpriseFieldVMs.ToList();
            for (int i = 0; i < newEnterpriseFields.Count; i++)
            {
                _enterpriseFieldService.Add(new EnterpriseFieldViewModel()
                {
                    FieldId = newEnterpriseFields[i].FieldId,
                    EnterpriseId = enterprise.Id,
                    Status = Status.Active,
                });
            }
        }

        public void Delete(int id)
        {
            _enterpriseRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
