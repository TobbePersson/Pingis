using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Mvc02.Models.ViewModels;

namespace Mvc02.Services

{
    [Authorize]
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
            if(addrole.Role != null)
            {
                IdentityRole role1 = new IdentityRole(addrole.Role);
                var checkRole = await _roleManager.FindByNameAsync(addrole.Role);
                var role = await _roleManager.CreateAsync(role1);
                if(addrole.Email != null)
                {
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(addrole.Email);
                    var addRoleToUser = await _userManager.AddToRoleAsync(identityUser, addrole.Role);
                }
            }
            else
            {

                var role = await _roleManager.FindByIdAsync(addrole.RoleInformation.Id);
                IdentityUser identityUser = await _userManager.FindByEmailAsync(addrole.Email);
                var addRoleToUser = await _userManager.AddToRoleAsync(identityUser, role.Name);
               
            }

            return true;

        }

        internal async Task<IdentityRole> GetRoleById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return role;
        }

        internal async Task<bool> Update(IdentityRole y, string oldRole)
        {
            var role = await _roleManager.FindByIdAsync(y.Id);

            var userInRole = await _userManager.GetUsersInRoleAsync(oldRole);

            var t = await _roleManager.UpdateAsync(y);

            foreach (var item in userInRole)
            {
                var setNewUserRole = await _userManager.AddToRoleAsync(item, y.Name);

            }
            
            return true;

        }

        internal async Task<bool> DeleteRole(string role)
        {
            var item = await _roleManager.FindByNameAsync(role);

            var users = await _userManager.GetUsersInRoleAsync(role);

            var delete = await _roleManager.DeleteAsync(item);

            var roles =  _roleManager.Roles;
            var name = "";
            var iduser = new IdentityUser();
            foreach (var r in roles)
            {
                if(r.Name.ToLower() != "admin" || r.Name.ToLower() != "master")
                {
                    foreach (var user in users)
                    {
                        name = r.Name;
                        iduser = user;
                    }
                    break;
                }
            }
            var setRole = await _userManager.AddToRoleAsync(iduser, name);

            return true;
        }

        internal async Task<bool> DeleteUser(string id)
        {
            var getUser = await _userManager.FindByIdAsync(id);
            var deleteUser = await _userManager.DeleteAsync(getUser);

            if (deleteUser.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
