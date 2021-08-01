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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class EnterpriseFieldService : IEnterpriseFieldService
    {
        private IEnterpriseFieldRepository _enterpriseFieldRepository;
        private IUnitOfWork _unitOfWork;

        public EnterpriseFieldService(IEnterpriseFieldRepository enterpriseFieldRepository,
            IUnitOfWork unitOfWork)
        {
            _enterpriseFieldRepository = enterpriseFieldRepository;
            _unitOfWork = unitOfWork;
        }
        public List<EnterpriseFieldViewModel> GetAllByFieldId(int fieldId)
        {
            return _enterpriseFieldRepository.FindAll()
                .Where(x => x.Status == Status.Active && x.FieldId == fieldId)
                .ProjectTo<EnterpriseFieldViewModel>().ToList();
        }

        public List<EnterpriseFieldViewModel> GetAllByEnterpriseId(int enterpriseId)
        {
            return _enterpriseFieldRepository.FindAll()
               .Where(x => x.Status == Status.Active && x.EnterpriseId == enterpriseId)
               .ProjectTo<EnterpriseFieldViewModel>().ToList();
        }

        public EnterpriseFieldViewModel GetById(int id)
        {
            return Mapper.Map<EnterpriseField, EnterpriseFieldViewModel>(_enterpriseFieldRepository.FindById(id));
        }

        public void Add(EnterpriseFieldViewModel enterpriseFieldVm)
        {
            var enterpriseField = Mapper.Map<EnterpriseFieldViewModel, EnterpriseField>(enterpriseFieldVm);
            _enterpriseFieldRepository.Add(enterpriseField);
        }

        public void Update(EnterpriseFieldViewModel enterpriseFieldVm)
        {
            var enterpriseField = Mapper.Map<EnterpriseFieldViewModel, EnterpriseField>(enterpriseFieldVm);
            _enterpriseFieldRepository.Update(enterpriseField);
        }

        public void Delete(int id)
        {
            _enterpriseFieldRepository.Remove(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
