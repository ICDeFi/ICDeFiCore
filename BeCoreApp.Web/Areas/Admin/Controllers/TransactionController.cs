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
    public class TransactionController : BaseController
    {
        public readonly ITransactionService _transactionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITRONService _tronService;
        private readonly IBlockChainService _blockChainService;
        public TransactionController(
            IBlockChainService blockChainService,
            UserManager<AppUser> userManager,
            ITRONService tronService,
            ITransactionService transactionService
            )
        {
            _blockChainService = blockChainService;
            _userManager = userManager;
            _tronService = tronService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetSpecificClaim("UserId");
            var appUser = await _userManager.FindByIdAsync(userId);
            return View();
        }
        public class WithdrawModel
        {
            public string AddressReceiving { get; set; }
            public decimal Amount { get; set; }
        }

        public class BuyModel
        {
            public decimal Amount { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(string modelJson)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<WithdrawModel>(modelJson);

                var userId = User.GetSpecificClaim("UserId");
                var appUser = await _userManager.FindByIdAsync(userId);

                if (appUser == null)
                    return new OkObjectResult(new GenericResult(false, "Account does not exist."));

                var validateAddress = await _tronService.ValidateAddress(model.AddressReceiving);
                if (validateAddress.success == false)
                {
                    return new OkObjectResult(new GenericResult(false, "The address receiving is not in the standard TRC20 format!"));
                }

                //if (model.Amount > appUser.WalletTikTokTrc20)
                //{
                //    return new OkObjectResult(new GenericResult(false,
                //                        $"Your balance TikTok (TT) is not enough."));
                //}

                if (model.Amount < 20000)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "Withraw TikTok (TT) amount minimum is 20,000TT"));
                }

                var transactionAmount = _transactionService.
                    GetUserAmountByTransactionType(userId)
                    .Where(x => x.Type == TransactionType.WithdrawTRX && x.DateCreated.Date == DateTime.Now.Date)
                    .Sum(x => x.Amount);

                var totalWithdrawToday = transactionAmount + model.Amount;
                if (totalWithdrawToday > 10000000)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "Withraw TikTok (TT) maximum is 10000000TT on day"));
                }


                BigInteger balanceTTTransfer = (BigInteger)(model.Amount * 1000000);

                var transactionReceipt = await _tronService.EasyTransferAssetByPrivate(
                     CommonConstants.TRONViTongPrivateKey, model.AddressReceiving,
                     CommonConstants.TRONDOLPContract, balanceTTTransfer);

                if (transactionReceipt.success == true)
                {
                    var result = await _userManager.UpdateAsync(appUser);

                    if (result.Succeeded)
                    {
                        var transtionLog = new TransactionViewModel()
                        {
                            AppUserId = appUser.Id,
                            TransactionHas = transactionReceipt.result,
                            Type = TransactionType.WithdrawDOLP,
                            Amount = model.Amount,
                            DateCreated = DateTime.Now,
                            AddressTo = model.AddressReceiving,
                        };
                        _transactionService.Add(transtionLog);
                        _transactionService.Save();
                    }
                }
                else
                {
                    return new OkObjectResult(new GenericResult(false, "There was a problem with the transfer TikTok (TT) to address receiving."));
                }

                return new OkObjectResult(new GenericResult(true,
                    $"Withdraw TikTok (TT) to address receiving {model.AddressReceiving} is success."));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(string modelJson)
        {
            try
            {
                var userId = User.GetSpecificClaim("UserId");
                var appUser = await _userManager.FindByIdAsync(userId);

                if (appUser == null)
                    return new OkObjectResult(new GenericResult(false, "Account does not exist."));

                var model = JsonConvert.DeserializeObject<BuyModel>(modelJson);
                if (model.Amount < 50)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "TRX Amount minimum is 50TRX"));
                }

                if (model.Amount > 50000)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "TRX Amount maximum is 50000TRX"));
                }

                var walletTrx = await _tronService.GetBalanceByAddress(appUser.TRXAddressBase58);
                if (walletTrx == null)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "There was a problem loading the balance value..."));
                }

                var balanceTrxDeposit = decimal.Parse(walletTrx.result) / 1000000;

                if (balanceTrxDeposit < model.Amount)
                {
                    return new OkObjectResult(new GenericResult(false,
                        $"Your account does not have sufficient quota {model.Amount}TRX"));
                }

                var coinMarKetCapInfo = _blockChainService.GetListingLatest(1, 50, "USD");
                if (coinMarKetCapInfo == null)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "There was a problem loading the currency value..."));
                }

                var dataTRX = coinMarKetCapInfo.data.Find(x => x.symbol == "TRX");
                if (dataTRX == null)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "There was a problem loading the currency value..."));
                }

                var priceTRX = dataTRX.quote.USD.price;

                decimal ttPrice = 0.00005m;

                decimal totalPayment = Math.Round(model.Amount * priceTRX, 5);

                var ttReceivingAmount = Math.Round(totalPayment / ttPrice, 5);

                BigInteger balanceTRXTransfer = (BigInteger)((model.Amount - 0.2M) * 1000000);

                var transactionReceipt = await _tronService.EasyTransferByPrivate(
                      appUser.TRXPrivateKey, CommonConstants.TRONViTongPublishKey, balanceTRXTransfer);

                if (transactionReceipt.success == true)
                {
                    var bonus = ttReceivingAmount * 0.1M;

                    var result = await _userManager.UpdateAsync(appUser);

                    if (result.Succeeded == true)
                    {
                        var transtionLog = new TransactionViewModel()
                        {
                            AppUserId = appUser.Id,
                            TransactionHas = transactionReceipt.result,
                            Type = TransactionType.Buy,
                            Amount = ttReceivingAmount + bonus,
                            DateCreated = DateTime.Now,
                            AddressTo = "Wallet TikTok (TT)",
                        };

                        _transactionService.Add(transtionLog);
                        _transactionService.Save();


                        var referralF1 = appUser.ReferralId.HasValue ? await _userManager.FindByIdAsync(appUser.ReferralId.Value.ToString()) : null;
                        if (referralF1 != null && !referralF1.IsSystem)
                        {
                            decimal expectTRXDepositF1 = model.Amount * 0.1M;
                            BigInteger balanceTRXTransferF1 = (BigInteger)(expectTRXDepositF1 * 1000000);

                            var transactionReceiptF1 = await _tronService.EasyTransferByPrivate(
                       CommonConstants.TRONViTongPrivateKey, referralF1.TRXAddressBase58, balanceTRXTransferF1);

                            var referralF2 = referralF1.ReferralId.HasValue ? await _userManager.FindByIdAsync(referralF1.ReferralId.ToString()) : null;
                            if (referralF2 != null && !referralF2.IsSystem)
                            {
                                decimal expectTRXDepositF2 = model.Amount * 0.05M;
                                BigInteger balanceTRXTransferF2 = (BigInteger)(expectTRXDepositF2 * 1000000);

                                var transactionReceiptF2 = await _tronService.EasyTransferByPrivate(
                           CommonConstants.TRONViTongPrivateKey, referralF2.TRXAddressBase58, balanceTRXTransferF2);

                                var referralF3 = referralF2.ReferralId.HasValue ? await _userManager.FindByIdAsync(referralF2.ReferralId.ToString()) : null;
                                if (referralF3 != null && !referralF3.IsSystem)
                                {
                                    decimal expectTRXDepositF3 = model.Amount * 0.03M;
                                    BigInteger balanceTRXTransferF3 = (BigInteger)(expectTRXDepositF3 * 1000000);

                                    var transactionReceiptF3 = await _tronService.EasyTransferByPrivate(
                               CommonConstants.TRONViTongPrivateKey, referralF3.TRXAddressBase58, balanceTRXTransferF3);

                                }
                            }
                        }
                    }
                    else
                    {
                        return new OkObjectResult(new GenericResult(false, $"There was a problem with the transfer {ttReceivingAmount + bonus}TT to Wallet TikTok TT"));
                    }
                }
                else
                {
                    return new OkObjectResult(new GenericResult(false, "There was a problem with the transfer TRX to Wallet System."));
                }

                return new OkObjectResult(new GenericResult(true,
                    $"Buy {ttReceivingAmount + (ttReceivingAmount * 0.1M)}TT to Wallet TikTok TT is success."));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }

        public class GetTRXPaymentAmountModel
        {
            public decimal Amount { get; set; }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetTRXPaymentAmount(string modelJson)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<GetTRXPaymentAmountModel>(modelJson);

                decimal ttPrice = 0.00005m;

                var coinMarKetCapInfo = _blockChainService.GetListingLatest(1, 50, "USD");
                if (coinMarKetCapInfo == null)
                {
                    return new OkObjectResult(new GenericResult(false, "There was a problem loading the currency value..."));
                }

                var dataTRX = coinMarKetCapInfo.data.Find(x => x.symbol == "TRX");
                if (dataTRX == null)
                {
                    return new OkObjectResult(new GenericResult(false,
                        "There was a problem loading the currency value..."));
                }

                var priceTRX = dataTRX.quote.USD.price;

                decimal totalPayment = Math.Round(model.Amount * priceTRX, 5);

                var ttReceivingAmount = Math.Round(totalPayment / ttPrice, 5);

                var bonus = ttReceivingAmount * 0.1M;

                return new OkObjectResult(new GenericResult(true, ttReceivingAmount + bonus));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult> DisplayUXByPlatform(int platformId)
        {
            var userId = User.GetSpecificClaim("UserId");
            var appUser = await _userManager.FindByIdAsync(userId);

            if (platformId == 0)
            {
                var model = new WalletViewModel()
                {
                };
                return PartialView("_DisplayWallet_0", model);
            }
            else
            {
                var model = new WalletViewModel();
                var walletTRX = await _tronService.GetBalanceByAddress(appUser.TRXAddressBase58);
                model.WalletTRX = 0;
                if (walletTRX.success)
                {
                    model.WalletTRX = decimal.Parse(walletTRX.result) / 1000000;
                }

                return PartialView("_DisplayWallet_2", model);
            }
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            string appUserId = string.Empty;

            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() == "customer")
            {
                appUserId = User.GetSpecificClaim("UserId");
            }

            var model = _transactionService.GetAllPaging(keyword, appUserId, page, pageSize);
            return new OkObjectResult(model);
        }
    }
}