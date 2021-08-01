using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Common;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository,
            IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _feedbackRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Title.Contains(keyword) || x.FullName.Contains(keyword)
                || x.Email.Contains(keyword) || x.Phone.Contains(keyword));
            }

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
                .Take(pageSize).Select(x => new FeedbackViewModel()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                    Message = x.Message,
                    Phone = x.Phone,
                    Status = x.Status,
                    Title = x.Title,
                    Type = x.Type,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified
                }).ToList();

            var paginationSet = new PagedResult<FeedbackViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public FeedbackViewModel GetById(int id)
        {
            var model = _feedbackRepository.FindById(id);
            if (model == null)
                return null;

            if (model.Type == FeedbackType.New)
            {
                model.Type = FeedbackType.Watched;
                model.DateModified = DateTime.Now;
                _feedbackRepository.Update(model);
                SaveChanges();
            }

            return new FeedbackViewModel()
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Message = model.Message,
                Phone = model.Phone,
                Status = model.Status,
                Title = model.Title,
                Type = model.Type,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified
            };
        }

        public void Add(FeedbackViewModel model)
        {
            var feedback = new Feedback()
            {
                Title = model.Title,
                Email = model.Email,
                FullName = model.FullName,
                Message = model.Message,
                Phone = model.Phone,
                Status = Status.Active,
                Type = FeedbackType.New,
                CreatedBy = "Người Dùng",
                ModifiedBy = "Người Dùng",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _feedbackRepository.Add(feedback);
        }

        public void UpdateType(int id, string modifiedBy)
        {
            var model = _feedbackRepository.FindById(id);
            if (model != null)
            {
                model.Type = FeedbackType.Responded;
                model.DateModified = DateTime.Now;
                model.ModifiedBy = modifiedBy;
                _feedbackRepository.Update(model);
            }
        }

        public void Delete(int id)
        {
            _feedbackRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

    }
}
