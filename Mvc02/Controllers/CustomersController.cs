using Microsoft.AspNetCore.Mvc;
using Mvc02.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc02.Controllers
{
    public class CustomersController : Controller
    { 
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Product);
        }
    }
}
