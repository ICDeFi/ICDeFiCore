using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.BlockChain;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Extensions;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class WalletTransferController : BaseController
    {
        public readonly IWalletTransferService _walletTransferService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITRONService _tronService;
        private readonly IBlockChainService _blockChainService;
        public WalletTransferController(
            IBlockChainService blockChainService,
            UserManager<AppUser> userManager,
            ITRONService tronService,
            IWalletTransferService walletTransferService
            )
        {
            _blockChainService = blockChainService;
            _userManager = userManager;
            _tronService = tronService;
            _walletTransferService = walletTransferService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public class TransferModel
        {
            public int TotalWallet { get; set; }
            public decimal Amount { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutoTransfer(string modelJson)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<TransferModel>(modelJson);

                for (int i = 1; i <= model.TotalWallet; i++)
                {
                    var accountTrc20 = await _tronService.GenerateAddress();

                    var walletTransfer = new WalletTransferViewModel
                    {
                        PrivateKey = accountTrc20.result.privateKey,
                        PublishKey = accountTrc20.result.publicKey,
                        AddressBase58 = accountTrc20.result.address.base58,
                        AddressHex = accountTrc20.result.address.hex,
                        Amount=model.Amount
                    };

                    BigInteger balanceTTTransfer = (BigInteger)(model.Amount * 1000000);

                    var transactionReceipt = await _tronService.EasyTransferAssetByPrivate(
                         CommonConstants.TRONViTongPrivateKey, accountTrc20.result.address.base58,
                         CommonConstants.TRONDOLPContract, balanceTTTransfer);

                    if (transactionReceipt.success == true)
                    {
                        _walletTransferService.Add(walletTransfer);
                        _walletTransferService.Save();
                    }
                }

                return new OkObjectResult(new GenericResult(true,
                        $"Auto Transfer TikTok (TT) to {model.TotalWallet} addresses is success."));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }


        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _walletTransferService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
    }
}