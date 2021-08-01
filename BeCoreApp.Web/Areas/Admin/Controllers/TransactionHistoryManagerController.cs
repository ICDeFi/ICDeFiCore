using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Enums;
using BeCoreApp.Extensions;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class TransactionHistoryManagerController : BaseController
    {
        public ITransactionHistoryService _transactionHistoryService;
        private readonly ILogger<WalletController> _logger;

        public TransactionHistoryManagerController(
            ILogger<WalletController> logger,
            ITransactionHistoryService transactionHistoryService
            )
        {
            _logger = logger;
            _transactionHistoryService = transactionHistoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _transactionHistoryService.GetAllPaging(keyword, null, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _transactionHistoryService.GetById(id);
            return new OkObjectResult(model);
        }
    }
}