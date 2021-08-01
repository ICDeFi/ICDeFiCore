using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.System;
using BeCoreApp.Application.ViewModels.Valuesshare;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Enums;
using BeCoreApp.Utilities.Constants;
using BeCoreApp.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlockChainService _blockChainService;
        public UserService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IBlockChainService blockChainService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _blockChainService = blockChainService;
        }

        #region Customer

        public PagedResult<AppUserViewModel> GetAllCustomerPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _userManager.Users.Where(x => x.IsSystem == false);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.FullName.Contains(keyword)
                || x.UserName.Contains(keyword)
                || x.PhoneNumber.Contains(keyword)
                || x.Email.Contains(keyword));

            int totalRow = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new AppUserViewModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Avatar = x.Avatar,
                    EmailConfirmed = x.EmailConfirmed,
                    BirthDay = x.BirthDay.ToString(),
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    DateCreated = x.DateCreated,
                }).ToList();

            var paginationSet = new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public async Task<NetworkViewModel> GetNetworkInfo(string id)
        {
            var customer = await _userManager.FindByIdAsync(id);
            var customerReferal = await _userManager.FindByIdAsync(customer.ReferralId.ToString());

            var model = new NetworkViewModel();
            model.FullName = customer.FullName;
            model.Email = customer.Email;
            model.Member = customer.UserName;
            model.Referal = customerReferal.UserName;
            model.PhoneNumber = customer.PhoneNumber;
            model.ReferalLink = $"https://icdefi.org/Admin/Account/Register/{customer.Id}";

            var userList = _userManager.Users.Where(x => x.EmailConfirmed == true && x.IsSystem == false);

            var f1Customers = userList.Where(x => x.ReferralId == customer.Id);
            model.TotalF1 = f1Customers.Count();
            model.TotalMember += f1Customers.Count();

            foreach (var f1Customer in f1Customers)
            {
                var f2Customers = userList.Where(x => x.ReferralId == f1Customer.Id);
                model.TotalF2 += f2Customers.Count();
                model.TotalMember += f2Customers.Count();

                foreach (var f2Customer in f2Customers)
                {
                    var f3Customers = userList.Where(x => x.ReferralId == f2Customer.Id);
                    model.TotalF3 += f3Customers.Count();
                    model.TotalMember += f3Customers.Count();

                    foreach (var f3Customer in f3Customers)
                    {
                        var f4Customers = userList.Where(x => x.ReferralId == f3Customer.Id);
                        model.TotalF4 += f4Customers.Count();
                        model.TotalMember += f4Customers.Count();

                        foreach (var f4Customer in f4Customers)
                        {
                            var f5Customers = userList.Where(x => x.ReferralId == f4Customer.Id);
                            model.TotalF5 += f5Customers.Count();
                            model.TotalMember += f5Customers.Count();
                        }
                    }
                }
            }

            return model;
        }

        public PagedResult<AppUserViewModel> GetCustomerReferralPagingAsync(string customerId, int refIndex, string keyword, int page, int pageSize)
        {
            IQueryable<AppUser> dataCustomers = null;
            var userList = _userManager.Users.Where(x => x.EmailConfirmed == true && x.IsSystem == false);

            if (!string.IsNullOrEmpty(keyword))
                userList = userList.Where(x => x.FullName.Contains(keyword) || x.UserName.Contains(keyword) || x.Email.Contains(keyword));

            var f1Customers = userList.Where(x => x.ReferralId == new Guid(customerId));
            if (refIndex == 1)
            {
                dataCustomers = f1Customers;
            }
            else
            {
                var f1Ids = f1Customers.Select(x => x.Id).ToList();
                var f2Customers = userList.Where(x => f1Ids.Contains(x.ReferralId.Value));
                if (refIndex == 2)
                {
                    dataCustomers = f2Customers;
                }
                else
                {
                    var f2Ids = f2Customers.Select(x => x.Id).ToList();
                    var f3Customers = userList.Where(x => f2Ids.Contains(x.ReferralId.Value));
                    if (refIndex == 3)
                    {
                        dataCustomers = f3Customers;
                    }
                    else
                    {
                        var f3Ids = f3Customers.Select(x => x.Id).ToList();
                        var f4Customers = userList.Where(x => f3Ids.Contains(x.ReferralId.Value));
                        if (refIndex == 4)
                        {
                            dataCustomers = f4Customers;
                        }
                        else
                        {
                            var f4Ids = f4Customers.Select(x => x.Id).ToList();
                            var f5Customers = userList.Where(x => f4Ids.Contains(x.ReferralId.Value));
                            if (refIndex == 5)
                            {
                                dataCustomers = f5Customers;
                            }
                        }
                    }
                }
            }

            int totalRow = dataCustomers.Count();
            var data = dataCustomers.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new AppUserViewModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Avatar = x.Avatar,
                    EmailConfirmed = x.EmailConfirmed,
                    BirthDay = x.BirthDay.ToString(),
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    ReferralId = x.ReferralId,
                    DateCreated = x.DateCreated
                }).ToList();

            return new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
        #endregion

        #region User System
        public PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _userManager.Users.Where(x => x.IsSystem);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.FullName.Contains(keyword) || x.UserName.Contains(keyword) || x.Email.Contains(keyword));

            int totalRow = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new AppUserViewModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Avatar = x.Avatar,
                    BirthDay = x.BirthDay.ToString(),
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    DateCreated = x.DateCreated
                }).ToList();

            var paginationSet = new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public List<AppUserTreeViewModel> GetTreeAll()
        {
            var listData = _userManager.Users.Where(x => x.EmailConfirmed == true)
                .Select(x => new AppUserTreeViewModel()
                {
                    id = x.Id,
                    text = x.UserName,
                    icon = "fa fa-folder",
                    state = new AppUserTreeState { opened = true },
                    data = new AppUserTreeData
                    {
                        referralId = x.ReferralId,
                        rootId = x.Id,
                        userName = x.UserName,
                        fullName = x.FullName,
                        avatar = x.Avatar,
                        dateCreated = x.DateCreated,
                        email = x.Email,
                        emailConfirmed = x.EmailConfirmed,
                        isSystem = x.IsSystem,
                        phoneNumber = x.PhoneNumber,
                        status = x.Status,
                    }
                });

            if (listData.Count() == 0)
                return new List<AppUserTreeViewModel>();

            var groups = listData.GroupBy(i => i.data.referralId);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue)
                    .ToDictionary(g => g.Key.Value, g => g.ToList());

                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }
            return roots;
        }

        private void AddChildren(AppUserTreeViewModel root, IDictionary<Guid, List<AppUserTreeViewModel>> source)
        {
            if (source.ContainsKey(root.id))
            {
                root.children = source[root.id].ToList();
                for (int i = 0; i < root.children.Count; i++)
                    AddChildren(root.children[i], source);
            }
            else
            {
                root.icon = "fa fa-file m--font-warning";
                root.children = new List<AppUserTreeViewModel>();
            }
        }

        public async Task<AppUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var userVm = new AppUserViewModel()
            {
                Id = user.Id,
                EmailConfirmed = user.EmailConfirmed,

                TRXPrivateKey = user.TRXPrivateKey,
                TRXPublishKey = user.TRXPublishKey,
                TRXAddressBase58 = user.TRXAddressBase58,
                TRXAddressHex = user.TRXAddressHex,
                DOLPBalance = user.DOLPBalance,
                IsSystem = user.IsSystem,
                ByCreated = user.ByCreated,
                ByModified = user.ByModified,
                DateModified = user.DateModified,
                UserName = user.UserName,
                Avatar = user.Avatar,
                BirthDay = user.BirthDay.ToString(),
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                DateCreated = user.DateCreated,
                Roles = roles.ToList()
            };

            return userVm;
        }

        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = new AppUser()
            {
                UserName = userVm.UserName,
                Avatar = userVm.Avatar,
                Email = userVm.Email,
                FullName = userVm.FullName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                PhoneNumber = userVm.PhoneNumber,
                DOLPBalance = 0,
                IsSystem = true
            };

            var result = await _userManager.CreateAsync(user, userVm.Password);
            if (result.Succeeded && userVm.Roles.Count > 0)
            {
                var appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                {
                    await _userManager.AddToRolesAsync(appUser, userVm.Roles.ToArray());
                }
            }

            return result.Succeeded;
        }


        public async Task UpdateAsync(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());

            //Remove current roles in db
            var currentRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, userVm.Roles.Except(currentRoles).ToArray());
            if (result.Succeeded)
            {
                string[] needRemoveRoles = currentRoles.Except(userVm.Roles).ToArray();
                await _userManager.RemoveFromRolesAsync(user, needRemoveRoles);

                user.FullName = userVm.FullName;
                user.Status = userVm.Status;
                user.Email = userVm.Email;
                user.PhoneNumber = userVm.PhoneNumber;
                user.DateModified = DateTime.Now;

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        #endregion
    }
}
