using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingApplication.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}