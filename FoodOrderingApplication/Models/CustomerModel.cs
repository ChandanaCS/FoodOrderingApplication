using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrderingApplication.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        [DisplayName("Enter Email: ")]
        [Required(ErrorMessage = "Email Field cannot be empty")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string CustomerEmail { get; set; }
        [DisplayName("Enter Password: ")]
        [Required(ErrorMessage = "Password Field cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}