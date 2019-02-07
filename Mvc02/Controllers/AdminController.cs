using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            return View();

        }

        public IActionResult AllRoles()
        {
            var listOfRoles = _auth.GetAllRoles();
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
            if(!ModelState.IsValid)
                return View("Index");

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
                    ModelState.AddModelError("Key", "Användaren kunde inte skapas");
                    return View("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Key", "Användaren finns inte.");
                return View("Index");

            }
        }

       
    }
}
