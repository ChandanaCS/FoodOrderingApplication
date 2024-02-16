using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrderingApplication.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [Display(Name = "Product Name ")]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}