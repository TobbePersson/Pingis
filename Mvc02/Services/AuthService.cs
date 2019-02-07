using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Mvc02.Models.ViewModels;

namespace Mvc02.Services

{
    [Authorize(Roles = "Bra")]
    public class AuthService

    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;



        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)

        {

            _userManager = userManager;

            _roleManager = roleManager;

            _signInManager = signInManager;

        }

        internal IQueryable<IdentityRole> GetAllRoles()
        {
            var listOfRoles = _roleManager.Roles;
            return listOfRoles;
        }

        internal async Task<bool> GetUserMail(string email)
        {
            var userMail = await _userManager.FindByEmailAsync(email);
            return userMail != null;
        }

        internal async Task<IList<IdentityUser>> GetUsersInRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            return users;
        }

        internal async Task<bool> AddRole(AddRoleVm addrole)
        {
            IdentityRole role1 = new IdentityRole(addrole.Role);
            var checkRole = await _roleManager.FindByNameAsync(addrole.Role);

            if(checkRole == null)
            {
                var role = await _roleManager.CreateAsync(role1);
                if(role.Succeeded)
                {
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(addrole.Email);
                    var addRoleToUser = await _userManager.AddToRoleAsync(identityUser, addrole.Role);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                IdentityUser identityUser = await _userManager.FindByEmailAsync(addrole.Email);
                var addRoleToUser = await _userManager.AddToRoleAsync(identityUser, addrole.Role);
                return true;
            }

        }

    }

}
