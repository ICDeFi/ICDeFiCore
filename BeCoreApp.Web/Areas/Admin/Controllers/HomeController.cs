using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Extensions;
using BeCoreApp.Models;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly INotifyService _notifyService;
        public readonly ITransactionService _transactionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITRONService _tronService;
        private readonly IBlockChainService _blockChainService;
        private readonly ILuckyRoomService _luckyRoomService;
        private readonly IAppUserLuckyRoomService _appUserLuckyRoomService;
        public HomeController(
            IAppUserLuckyRoomService appUserLuckyRoomService,
            ILuckyRoomService luckyRoomService,
            IBlockChainService blockChainService,
            UserManager<AppUser> userManager,
            ITRONService tronService,
            ITransactionService transactionService,
            INotifyService notifyService
            )
        {
            _appUserLuckyRoomService = appUserLuckyRoomService;
            _luckyRoomService = luckyRoomService;
            _blockChainService = blockChainService;
            _userManager = userManager;
            _tronService = tronService;
            _transactionService = transactionService;
            _notifyService = notifyService;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Notify = _notifyService.GetbyActive();
            return View(model);
        }
    }
}