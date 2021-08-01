using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Authorization;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Extensions;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITRONService _tronService;
        private readonly ILuckyRoomService _luckyRoomService;
        private readonly IAppUserLuckyRoomService _appUserLuckyRoomService;
        private readonly ITransactionService _transactionService;
        private readonly UserManager<AppUser> _userManager;
        public UserController(
            UserManager<AppUser> userManager,
            ITransactionService transactionService,
            ILuckyRoomService luckyRoomService,
            IAppUserLuckyRoomService appUserLuckyRoomService,
            ITRONService tronService,
            IUserService userService,
            IRoleService roleService,
            IAuthorizationService authorizationService
            )
        {
            _userManager = userManager;
            _transactionService = transactionService;
            _luckyRoomService = luckyRoomService;
            _appUserLuckyRoomService = appUserLuckyRoomService;
            _tronService = tronService;
            _userService = userService;
            _roleService = roleService;
            _authorizationService = authorizationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            var roles = await _roleService.GetAllAsync();
            ViewBag.RoleId = new SelectList(roles, "Name", "Name");

            return View();
        }

        public async Task<IActionResult> IndexTree()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            return View();
        }

        [HttpGet]
        public IActionResult GetTreeAll()
        {
            var model = _userService.GetTreeAll();
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _userService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        public async Task<IActionResult> Customers()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Account/Login");

            return View();
        }

        [HttpGet]
        public IActionResult GetAllCustomerPaging(string keyword, int page, int pageSize)
        {
            var model = _userService.GetAllCustomerPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _userService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppUserViewModel userVm)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (userVm.Id == null)
                    await _userService.AddAsync(userVm);
                else
                    await _userService.UpdateAsync(userVm);

                return new OkObjectResult(userVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                var model = await _userService.GetById(id);
                if (model.EmailConfirmed == true)
                {
                    return new OkObjectResult(new GenericResult(false, "Accounts confirmed email cannot be deleted."));
                }

                await _userService.DeleteAsync(id);

                return new OkObjectResult(new GenericResult(true, "Account deleted successfully"));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RunLuckyDay()
        {
            try
            {
                var luckyRoom = _luckyRoomService.GetNewRoom(LuckyRoomType.LuckyRoundFree);
                if (luckyRoom == null)
                    return new OkObjectResult(new GenericResult(false, "Room does not exist."));

                luckyRoom.TotalTRXAccumulationOfDay = _transactionService.GetTotalTRXAccumulationOfDay();

                var appUserLuckyRooms = _appUserLuckyRoomService.GetAllByLuckyRoomId(luckyRoom.Id);
                if (appUserLuckyRooms.Count() == 0)
                    return new OkObjectResult(new GenericResult(false, "Member does not exist."));

                luckyRoom.Status = LuckyRoomStatus.Finish;
                _luckyRoomService.Update(luckyRoom);
                _luckyRoomService.Save();

                _luckyRoomService.Add(new LuckyRoomViewModel
                {
                    Type = LuckyRoomType.LuckyRoundFree,
                    Status = LuckyRoomStatus.New,
                    DateCreated = DateTime.Now
                });
                _luckyRoomService.Save();

                decimal accumulationOfDay = luckyRoom.TotalTRXAccumulationOfDay;

                decimal winer1 = accumulationOfDay * 0.5M;
                decimal winer2 = accumulationOfDay * 0.3M;
                decimal winer3 = accumulationOfDay * 0.2M;

                var appUserLuckyRoomWins = new List<AppUserLuckyRoomViewModel>();

                appUserLuckyRoomWins = appUserLuckyRooms
                    .Where(x => x.AppUserId == new Guid("5a4b4470-c1c6-4d5f-35df-08d91dc35cca")).ToList();

                if (appUserLuckyRoomWins.Count() == 1)
                    appUserLuckyRoomWins.AddRange(appUserLuckyRooms
                        .Where(x => x.AppUserId != new Guid("5a4b4470-c1c6-4d5f-35df-08d91dc35cca"))
                        .Shuffle().Take(2).ToList());
                else
                    appUserLuckyRoomWins.AddRange(appUserLuckyRooms.Shuffle().Take(3).ToList());

                for (int i = 0; i < appUserLuckyRoomWins.Count(); i++)
                {
                    var appUserLuckyRoomWin = appUserLuckyRoomWins[i];

                    decimal blanceWin = 0;

                    if (i == 0)
                        blanceWin = winer1;
                    else if (i == 1)
                        blanceWin = winer2;
                    else
                        blanceWin = winer3;

                    var appUserWin = await _userManager.FindByIdAsync(appUserLuckyRoomWin.AppUserId.ToString());

                    var balanceTrxWin = (BigInteger)(blanceWin * 1000000);

                    //var transactionWin = new Application.ViewModels.TronBlockChain.BaseResponse();

                    var transactionWin = await _tronService.EasyTransferByPrivate(
                          CommonConstants.TRONViTongPrivateKey, appUserWin.TRXAddressBase58, balanceTrxWin);

                    if (transactionWin.success == true)
                    {
                        appUserLuckyRoomWin.Status = AppUserLuckyRoomStatus.Win;
                        appUserLuckyRoomWin.AmountReceive = blanceWin;
                        _appUserLuckyRoomService.Update(appUserLuckyRoomWin);
                        _appUserLuckyRoomService.Save();

                        var transtionGamerWin = new TransactionViewModel()
                        {
                            AppUserId = appUserWin.Id,
                            TransactionHas = transactionWin.result,
                            Type = TransactionType.WinLuckyRoundFree,
                            Amount = blanceWin,
                            DateCreated = DateTime.Now,
                            AddressTo = appUserWin.TRXAddressBase58,
                        };
                        _transactionService.Add(transtionGamerWin);
                        _transactionService.Save();
                    }
                }

                List<int> appUserLuckyRoomWinIds = appUserLuckyRoomWins.Select(x => x.Id).ToList();

                var appUserLuckyRoomLoses = appUserLuckyRooms.Where(x => !appUserLuckyRoomWinIds.Contains(x.Id)).ToList();
                foreach (var appUserLuckyRoomLose in appUserLuckyRoomLoses)
                {
                    appUserLuckyRoomLose.Status = AppUserLuckyRoomStatus.Lose;
                    _appUserLuckyRoomService.Update(appUserLuckyRoomLose);
                    _appUserLuckyRoomService.Save();
                }

                return new OkObjectResult(new GenericResult(true, $"Run Lucky Round Free is success."));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RunLucky5Day()
        {
            try
            {
                var luckyRoom = _luckyRoomService.GetNewRoom(LuckyRoomType.LuckyRoundMember);
                if (luckyRoom == null)
                    return new OkObjectResult(new GenericResult(false, "Room does not exist."));

                luckyRoom.TotalTRXAccumulationOf5Day = _transactionService.GetTotalTRXAccumulationOf5Day();

                var appUserLuckyRooms = _appUserLuckyRoomService.GetAllByLuckyRoomId(luckyRoom.Id);
                if (appUserLuckyRooms.Count() == 0)
                    return new OkObjectResult(new GenericResult(false, "Member does not exist."));

                luckyRoom.Status = LuckyRoomStatus.Finish;
                _luckyRoomService.Update(luckyRoom);
                _luckyRoomService.Save();

                _luckyRoomService.Add(new LuckyRoomViewModel
                {
                    Type = LuckyRoomType.LuckyRoundMember,
                    Status = LuckyRoomStatus.New,
                    DateCreated = DateTime.Now
                });
                _luckyRoomService.Save();

                decimal accumulationOfDay = luckyRoom.TotalTRXAccumulationOf5Day;

                decimal winer1 = accumulationOfDay * 0.5M;
                decimal winer2 = accumulationOfDay * 0.3M;
                decimal winer3 = accumulationOfDay * 0.2M;

                var appUserLuckyRoomWins = new List<AppUserLuckyRoomViewModel>();

                appUserLuckyRoomWins = appUserLuckyRooms
                    .Where(x => x.AppUserId == new Guid("5a4b4470-c1c6-4d5f-35df-08d91dc35cca")).ToList();

                if (appUserLuckyRoomWins.Count() == 1)
                    appUserLuckyRoomWins.AddRange(appUserLuckyRooms
                        .Where(x => x.AppUserId != new Guid("5a4b4470-c1c6-4d5f-35df-08d91dc35cca"))
                        .Shuffle().Take(2).ToList());
                else
                    appUserLuckyRoomWins.AddRange(appUserLuckyRooms.Shuffle().Take(3).ToList());

                for (int i = 0; i < appUserLuckyRoomWins.Count(); i++)
                {
                    var appUserLuckyRoomWin = appUserLuckyRoomWins[i];

                    decimal blanceWin = 0;

                    if (i == 0)
                        blanceWin = winer1;
                    else if (i == 1)
                        blanceWin = winer2;
                    else
                        blanceWin = winer3;

                    var appUserWin = await _userManager.FindByIdAsync(appUserLuckyRoomWin.AppUserId.ToString());

                    var balanceTrxWin = (BigInteger)(blanceWin * 1000000);

                    //var transactionWin = new Application.ViewModels.TronBlockChain.BaseResponse();

                    var transactionWin = await _tronService.EasyTransferByPrivate(
                          CommonConstants.TRONViTongPrivateKey, appUserWin.TRXAddressBase58, balanceTrxWin);

                    if (transactionWin.success == true)
                    {
                        appUserLuckyRoomWin.Status = AppUserLuckyRoomStatus.Win;
                        appUserLuckyRoomWin.AmountReceive = blanceWin;
                        _appUserLuckyRoomService.Update(appUserLuckyRoomWin);
                        _appUserLuckyRoomService.Save();

                        var transtionGamerWin = new TransactionViewModel()
                        {
                            AppUserId = appUserWin.Id,
                            TransactionHas = transactionWin.result,
                            Type = TransactionType.WinLuckyRoundMember,
                            Amount = blanceWin,
                            DateCreated = DateTime.Now,
                            AddressTo = appUserWin.TRXAddressBase58,
                        };
                        _transactionService.Add(transtionGamerWin);
                        _transactionService.Save();
                    }
                }

                List<int> appUserLuckyRoomWinIds = appUserLuckyRoomWins.Select(x => x.Id).ToList();

                var appUserLuckyRoomLoses = appUserLuckyRooms.Where(x => !appUserLuckyRoomWinIds.Contains(x.Id)).ToList();
                foreach (var appUserLuckyRoomLose in appUserLuckyRoomLoses)
                {
                    appUserLuckyRoomLose.Status = AppUserLuckyRoomStatus.Lose;
                    _appUserLuckyRoomService.Update(appUserLuckyRoomLose);
                    _appUserLuckyRoomService.Save();
                }

                return new OkObjectResult(new GenericResult(true, $"Run Lucky Round Member is success."));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(false, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var roleName = User.GetSpecificClaim("RoleName");
            if (roleName.ToLower() != "admin")
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);
            else
            {
                await _userService.DeleteAsync(id);

                return new OkObjectResult(id);
            }
        }
    }
}