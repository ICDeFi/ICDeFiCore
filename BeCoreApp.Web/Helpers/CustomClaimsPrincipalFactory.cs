using BeCoreApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeCoreApp.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        UserManager<AppUser> _userManager;
        RoleManager<AppRole> _roleManager;

        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByNameAsync(roles[0]);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim("UserName",user.UserName),
                new Claim("Email",user.Email),
                new Claim("FullName",user.FullName),
                new Claim("Avatar",user.Avatar??string.Empty),
                new Claim("Roles",string.Join(";",roles)),
                new Claim("RoleName",string.Join(";",roles)),
                new Claim("RoleId",role.Id.ToString()??string.Empty),
            });

            return principal;
        }
    }
}
