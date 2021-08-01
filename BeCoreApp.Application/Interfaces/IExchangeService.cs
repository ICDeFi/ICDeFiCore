using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Application.ViewModels.Valuesshare;
using BeCoreApp.Data.Entities;
using BeCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Interfaces
{
    public interface IExchangeService
    {
        void Add(ExchangeViewModel exchangeVm);
        bool CheckLimitExchange(string userId);
        bool CheckExchanged(string userId);
        void Save();
    }
}
