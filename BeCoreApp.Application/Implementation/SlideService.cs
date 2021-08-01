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
    public class SlideService : ISlideService
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SlideService(
            ISlideRepository slideRepository,
            IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<SlideViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            var query = _slideRepository.FindAll();

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


            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new SlideViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url=x.Url,
                    Image = x.Image,
                    Description = x.Description,
                    HotFlag = x.HotFlag,
                    Status = x.Status,
                    DateModified = x.DateModified,
                    DateCreated = x.DateCreated,
                }).ToList();

            return new PagedResult<SlideViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }


        public SlideViewModel Add(SlideViewModel model)
        {
            var slide = new Slide
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                Description = model.Description,
                Url = model.Url,
                HotFlag = model.HotFlag,
                Status = model.Status,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _slideRepository.Add(slide);

            return model;
        }

        public void Delete(int id) => _slideRepository.Remove(id);

        public List<SlideViewModel> GetLatests(int top)
        {
            return _slideRepository.FindAll()
                .OrderByDescending(x => x.DateModified).Take(top)
            .Select(x => new SlideViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Url = x.Url,
                Description = x.Description,
                HotFlag = x.HotFlag,
                Status = x.Status,
                DateModified = x.DateModified,
                DateCreated = x.DateCreated,
            }).ToList();
        }

        public List<SlideViewModel> GetHots(int top)
        {
            return _slideRepository.FindAll(x => x.HotFlag.Value)
                .OrderByDescending(x => x.DateModified).Take(top)
            .Select(x => new SlideViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Url = x.Url,
                Description = x.Description,
                HotFlag = x.HotFlag,
                Status = x.Status,
                DateModified = x.DateModified,
                DateCreated = x.DateCreated,
            }).ToList();
        }

        public SlideViewModel GetById(int id)
        {
            var model = _slideRepository.FindById(id);
            if (model == null)
                return null;

            return new SlideViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                Url = model.Url,
                Description = model.Description,
                HotFlag = model.HotFlag,
                Status = model.Status,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
            };
        }

        public void Save() => _unitOfWork.Commit();

        public void Update(SlideViewModel model)
        {
            var slideDb = _slideRepository.FindById(model.Id);
            if (slideDb != null)
            {
                slideDb.Id = model.Id;
                slideDb.Name = model.Name;
                slideDb.Image = model.Image;
                slideDb.Description = model.Description;
                slideDb.Url = model.Url;
                slideDb.HotFlag = model.HotFlag;
                slideDb.Status = model.Status;
                slideDb.DateModified = DateTime.Now;

                _slideRepository.Update(slideDb);
            }
        }
    }
}