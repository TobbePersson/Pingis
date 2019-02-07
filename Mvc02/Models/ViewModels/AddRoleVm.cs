using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc02.Models.ViewModels
{
    public class AddRoleVm
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        public List<string> roles { get; set; }
        public IList<IdentityUser> indentityusers { get; set; }
    }
}
