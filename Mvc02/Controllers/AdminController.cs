using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc02.Data;
using Mvc02.Models.ViewModels;
using Mvc02.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mvc02.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AuthService _auth;



        public AdminController(AuthService auth)

        {

            _auth = auth;

        }

        public IActionResult Index()
        {
            var listofRoles = GetAllRoles();

            var list = new List<SelectListItem>();

            foreach (var role in listofRoles)
            {
                list.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString()
                });
            }

            AddRoleVm vm = new AddRoleVm();

            vm.RoleItems = list;

            return View(vm);

        }

        public IActionResult AllRoles()
        {
            var listOfRoles = GetAllRoles();

            List<string> roles = new List<string>();

            foreach (var role in listOfRoles)
            {
                roles.Add(role.Name);
            }
            AddRoleVm vm = new AddRoleVm();
            vm.roles = roles;
            return View(vm);
        }

        public async Task<IActionResult> GetUsersInRole(string role) {
            AddRoleVm vm = new AddRoleVm();
            var users = await _auth.GetUsersInRole(role);
            vm.indentityusers = users;
            vm.Role = role;
            return View(vm);
        }

        public IActionResult AddRoleForUser(AddRoleVm addrole)
        {
            var userMail = _auth.GetUserMail(addrole.Email).Result;

            if (userMail == true)
            {
                var checkAdd =_auth.AddRole(addrole).Result;
                if(checkAdd == true)
                {
                    return View(addrole);
                }
                else
                {
                    ViewData["message"] = "Funkade inte";
                    return View("AddRole");
                }
            }
            else
            {
                ViewData["message"] = "Mail fanns inte";
                return View("AddRole");

            }
        }

        [HttpPost]
        public IActionResult AddRole(AddRoleVm rolename)
        {

            var checkAdd = _auth.AddRole(rolename).Result;

            var listOfRoles = GetAllRoles();

            List<string> roles = new List<string>();

            foreach (var item in listOfRoles)
            {
                roles.Add(item.Name);
            }
            AddRoleVm vm = new AddRoleVm();
            vm.roles = roles;
            return View("AllRoles", vm);

        }
        
        public async Task<IActionResult> UpdateRole(AddRoleVm addrole)
        {
            var role = await _auth.GetRoleById(addrole.RoleInformation.Id);
            AddRoleVm vm = new AddRoleVm();
            vm.RoleInformation = role;
            vm.RoleIdToUpdate = role.Id;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AddRoleVm x)
        {
            var oldRole = await _auth.GetRoleById(x.RoleIdToUpdate);

            oldRole.Name = x.Role;
            var answer = await _auth.Update(oldRole, x.Role);
            var listOfRoles = GetAllRoles();

            List<string> roles = new List<string>();

            foreach (var item in listOfRoles)
            {
                roles.Add(item.Name);
            }
            AddRoleVm vm = new AddRoleVm();
            vm.roles = roles;
            return View("AllRoles",vm);
        }
        
        public IActionResult UpdateAddRole()
        {
            var listofRoles = GetAllRoles();

            var list = new List<SelectListItem>();

            foreach (var role in listofRoles)
            {
                list.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString()
                });
            }

            AddRoleVm vm = new AddRoleVm();

            vm.RoleItems = list;

            return View(vm);
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            var listOfRoles = _auth.GetAllRoles();
            return listOfRoles;
        }

        public async Task<IActionResult> Delete(string role)
        {
            var result = await _auth.DeleteRole(role);
            var listOfRoles = GetAllRoles();

            List<string> roles = new List<string>();

            foreach (var item in listOfRoles)
            {
                roles.Add(item.Name);
            }
            AddRoleVm vm = new AddRoleVm();
            vm.roles = roles;
            return View("AllRoles", vm);
        }

        public async Task<IActionResult> DeleteUserById(string Id)
        {
            var deleteUser = await _auth.DeleteUser(Id);
            var viewModel = new DeleteUserVm();
            viewModel.status = deleteUser;

            return View(viewModel);
            
        }
    }
}
