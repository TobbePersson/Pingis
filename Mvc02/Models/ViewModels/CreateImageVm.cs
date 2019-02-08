using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc02.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc02.Models.ViewModels
{
    public class CreateImageVm
    {
        public IEnumerable<SelectListItem> AllProducts { get; set; }
      //  public List<Image> Images { get; set; }
        public Image Image { get; set; }
        public Product Product { get; set; }
    }
}
