using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Application.ViewModels.Valuesshare;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ExchangeService(IExchangeRepository exchangeRepository, IUnitOfWork unitOfWork)
        {
            _exchangeRepository = exchangeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(ExchangeViewModel exchangeVm)
        {
            var model = new Exchange()
            {
                AppUserId = exchangeVm.AppUserId,
                WalletFrom = exchangeVm.WalletFrom,
                WalletTo = exchangeVm.WalletTo,
                ExchangeDate = exchangeVm.ExchangeDate,
                Amount = exchangeVm.Amount
            };

            _exchangeRepository.Add(model);
        }

        public bool CheckLimitExchange(string userId)
        {
            DateTime rangeDate = new DateTime(2020, 7, 31);

            var exchanges = _exchangeRepository.FindAll(x => x.AppUserId == new Guid(userId)).ToList();

            int totalExchange = exchanges.Count(x => x.ExchangeDate.Date > rangeDate);
            if (totalExchange >= 30)
            {
                return true;
            }

            return false;
        }

        public bool CheckExchanged(string userId)
        {
            var model = _exchangeRepository.FindAll(x => x.AppUserId == new Guid(userId));

            if (model.FirstOrDefault(x => x.ExchangeDate.Date == DateTime.Now.Date) != null)
            {
                return true;
            }

            return false;
        }

        public void Save() => _unitOfWork.Commit();
    }
}
