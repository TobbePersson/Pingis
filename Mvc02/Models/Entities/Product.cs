using Mvc02.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc02.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Obligatoriskt")]
        public string Name { get; set; }

        //[Required(ErrorMessage ="Obligatoriskt")]
        [Range(0,1000,ErrorMessage ="Måste vara mellan 0 och 1000"), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "For sale")]
        public bool ForSale { get; set; }

        [Required(ErrorMessage = "Måste ha en beskrivning")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Måste fylla på lagersaldo")]
        public int AmountInStock { get; set; }

        public List<Image> Images { get; set; }

        public string FrontImage { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
