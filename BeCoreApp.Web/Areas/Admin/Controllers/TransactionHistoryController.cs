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
    public class TransactionHistoryController : BaseController
    {
        public ITransactionHistoryService _transactionHistoryService;
        private readonly ILogger<WalletController> _logger;

        public TransactionHistoryController(
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
            string appUserId = appUserId = User.GetSpecificClaim("UserId");
            var model = _transactionHistoryService.GetAllPaging(keyword, appUserId, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _transactionHistoryService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveVerify(string modelJson)
        {
            try
            {
                var transactionHistoryVm = JsonConvert.DeserializeObject<TransactionHistoryViewModel>(modelJson);

                var isExist = _transactionHistoryService.IsExist(transactionHistoryVm.TransactionHash);
                if (isExist)
                {
                    return new OkObjectResult(new GenericResult(false, $"Transaction Hash is exist."));
                }

                if (transactionHistoryVm.Amount > 50000000)
                {
                    return new OkObjectResult(new GenericResult(false, $"Verify transfer TIKTOK maximum is 50000000 Billion Billion."));
                }

                string appUserId = appUserId = User.GetSpecificClaim("UserId");
                transactionHistoryVm.AppUserId = new Guid(appUserId);
                transactionHistoryVm.Type = TransactionHistoryType.New;
                _transactionHistoryService.Add(transactionHistoryVm);
                _transactionHistoryService.Save();
                return new OkObjectResult(new GenericResult(true, $"Create verify transfer is success."));
            }
            catch (Exception ex)
            {
                _logger.LogError("TransactionHistoryController_SaveVerify: {0}", ex.Message);
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }
    }
}