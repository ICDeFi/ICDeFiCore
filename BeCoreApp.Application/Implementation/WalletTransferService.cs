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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeCoreApp.Application.Implementation
{
    public class WalletTransferService : IWalletTransferService
    {
        private readonly IWalletTransferRepository _walletTransferRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WalletTransferService
            (
            IWalletTransferRepository walletTransferRepository,
            IUnitOfWork unitOfWork
            )
        {
            _walletTransferRepository = walletTransferRepository;
            _unitOfWork = unitOfWork;
        }

        public PagedResult<WalletTransferViewModel> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            var query = _walletTransferRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.PrivateKey.Contains(keyword) || x.AddressBase58.Contains(keyword));

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x => new WalletTransferViewModel()
                {
                    Id = x.Id,
                    PrivateKey = x.PrivateKey,
                    AddressBase58 = x.AddressBase58,
                    AddressHex = x.AddressHex,
                    PublishKey = x.PublishKey,
                    Amount = x.Amount
                }).ToList();

            return new PagedResult<WalletTransferViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public void Add(WalletTransferViewModel model)
        {
            var transaction = new WalletTransfer()
            {
                PrivateKey = model.PrivateKey,
                AddressBase58 = model.AddressBase58,
                AddressHex = model.AddressHex,
                PublishKey = model.PublishKey,
                Amount = model.Amount
            };

            _walletTransferRepository.Add(transaction);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
