using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Blog;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Helpers;

namespace BeCoreApp.Application.Implementation
{
    public class NotifyService : INotifyService
    {
        private readonly INotifyRepository _notifyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotifyService(INotifyRepository notifyRepository, IUnitOfWork unitOfWork)
        {
            _notifyRepository = notifyRepository;
            _unitOfWork = unitOfWork;
        }

        public NotifyViewModel Add(NotifyViewModel notifyVm)
        {
            var notify = new Notify
            {
                Name = notifyVm.Name,
                MildContent = notifyVm.MildContent,
                Status = notifyVm.Status,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _notifyRepository.Add(notify);

            return notifyVm;
        }

        public NotifyViewModel GetbyActive()
        {
            var model = _notifyRepository
                .FindAll(x => x.Status == Status.Active).FirstOrDefault();

            if (model == null)
                return null;

            return new NotifyViewModel
            {
                Id = model.Id,
                Name = model.Name,
                MildContent = model.MildContent,
                Status = model.Status,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
            };
        }
        public NotifyViewModel GetFirst()
        {
            var model = _notifyRepository.FindAll().FirstOrDefault();
            if (model == null)
                return null;

            return new NotifyViewModel
            {
                Id = model.Id,
                Name = model.Name,
                MildContent = model.MildContent,
                Status = model.Status,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
            };
        }

        public void Save() => _unitOfWork.Commit();

        public void Update(NotifyViewModel notifyVm)
        {
            var notify = _notifyRepository.FindById(notifyVm.Id);
            notify.Name = notifyVm.Name;
            notify.MildContent = notifyVm.MildContent;
            notify.Status = notifyVm.Status;
            notify.DateModified = DateTime.Now;

            _notifyRepository.Update(notify);
        }
    }
}