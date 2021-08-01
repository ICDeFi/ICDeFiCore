using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class ContactService : IContactService
    {
        private IContactRepository _contactRepository;
        private IUnitOfWork _unitOfWork;

        public ContactService(IContactRepository contactRepository,
            IUnitOfWork unitOfWork)
        {
            this._contactRepository = contactRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(ContactViewModel pageVm)
        {
            pageVm.SeoAlias = TextHelper.UrlFriendly(pageVm.Name);
            var page = Mapper.Map<ContactViewModel, Contact>(CheckSeo(pageVm));
            _contactRepository.Add(page);
        }

        public ContactViewModel CheckSeo(ContactViewModel modeVm)
        {
            if (string.IsNullOrWhiteSpace(modeVm.SeoPageTitle))
                modeVm.SeoPageTitle = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoKeywords))
                modeVm.SeoKeywords = modeVm.Name;

            if (string.IsNullOrWhiteSpace(modeVm.SeoDescription))
                modeVm.SeoDescription = modeVm.Name;

            return modeVm;
        }

        public void Update(ContactViewModel pageVm)
        {
            pageVm.SeoAlias = TextHelper.UrlFriendly(pageVm.Name);
            var page = Mapper.Map<ContactViewModel, Contact>(CheckSeo(pageVm));
            _contactRepository.Update(page);
        }

        public void Delete(string id)
        {
            _contactRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ContactViewModel> GetAll()
        {
            return _contactRepository.FindAll().ProjectTo<ContactViewModel>().ToList();
        }

        public PagedResult<ContactViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _contactRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<ContactViewModel>()
            {
                Results = data.ProjectTo<ContactViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public ContactViewModel GetById(string id)
        {
            return Mapper.Map<Contact, ContactViewModel>(_contactRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }     
    }
}
