using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.Location;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Extensions;
using BeCoreApp.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionHistoryService
            (
            UserManager<AppUser> userManager,
            ITransactionHistoryRepository transactionHistoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _transactionHistoryRepository = transactionHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<TransactionHistoryViewModel> GetAllPaging(string keyword, string appUserId, int pageIndex, int pageSize)
        {
            var query = _transactionHistoryRepository.FindAll(x => x.AppUser);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.AppUser.FullName.Contains(keyword) || x.TransactionHash.Contains(keyword));

            if (!string.IsNullOrWhiteSpace(appUserId))
                query = query.Where(x => x.AppUserId.ToString() == appUserId);

            var totalRow = query.Count();
            var data = query.OrderBy(x => x.Type)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new TransactionHistoryViewModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Image = x.Image,
                    AppUserId = x.AppUserId,
                    AppUserName = x.AppUser.UserName,
                    TransactionHash = x.TransactionHash,
                    Note = x.Note,
                    Type = x.Type,
                    CreatedDate = x.CreatedDate,
                    TypeName = x.Type.GetDescription()
                }).ToList();

            return new PagedResult<TransactionHistoryViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public TransactionHistoryViewModel GetById(int id)
        {
            var transactionHistory = _transactionHistoryRepository.FindById(id, x => x.AppUser);
            var model = new TransactionHistoryViewModel()
            {
                Id = transactionHistory.Id,
                Note = transactionHistory.Note,
                Image = transactionHistory.Image,
                Amount = transactionHistory.Amount,
                AppUserName = transactionHistory.AppUser.UserName,
                AppUserId = transactionHistory.AppUserId,
                CreatedDate = transactionHistory.CreatedDate,
                UpdatedDate = transactionHistory.UpdatedDate,
                TransactionHash = transactionHistory.TransactionHash,
                Type = transactionHistory.Type,
                TypeName = transactionHistory.Type.GetDescription()
            };

            return model;
        }
        public bool IsExist(string transactionHash)
        {
            var transfers = _transactionHistoryRepository
                .FindAll(x => x.TransactionHash == transactionHash.Trim()
                && x.Type != TransactionHistoryType.Rejected);

            if (transfers.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public void Add(TransactionHistoryViewModel model)
        {
            var transactionHistory = new TransactionHistory()
            {
                Note = "",
                Image = model.Image,
                Amount = model.Amount,
                AppUserId = model.AppUserId,
                TransactionHash = model.TransactionHash,
                Type = model.Type,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _transactionHistoryRepository.Add(transactionHistory);
        }

        public void Reject(int id, string note)
        {
            var transactionHistory = _transactionHistoryRepository.FindById(id);
            transactionHistory.Note = note;
            transactionHistory.Type = TransactionHistoryType.Rejected;
            transactionHistory.UpdatedDate = DateTime.Now;
            _transactionHistoryRepository.Update(transactionHistory);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
