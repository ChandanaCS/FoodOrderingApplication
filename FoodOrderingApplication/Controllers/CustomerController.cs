using FoodOrdering_DataAccessLayer_Library;
using FoodOrderingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingApplication.Controllers
{
    public class CustomerController : Controller
    {
        int totalPrice;
        // GET: Customer
        public ActionResult Index()
        {
            AdminDAL dal = new AdminDAL();
            List<ProductList> productlist = dal.GetAllProducts();
            List<ProductModel> productmodels = new List<ProductModel>();
            foreach (ProductList product in productlist)
            {
                productmodels.Add(new ProductModel { ProductID = product.ProductID, ProductName = product.ProductName, Price = product.Price });
            }
            return View(productmodels);
            
        }
        private Dictionary<int, int> GetCartItems()
        {
            if (Session["CartItems"] == null)
            {
                Session["CartItems"] = new Dictionary<int, int>();
            }
            
            return (Dictionary<int, int>)Session["CartItems"];
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity, int total)
        {
            Dictionary<int, int> cartItems = GetCartItems();
            int totalPrice = Session["TotalPrice"] == null ? total : (int)Session["TotalPrice"] + total;
            Session["TotalPrice"] = totalPrice;
            if (cartItems.ContainsKey(productId))
            {
                //cartItems[productId] += quantity;
                cartItems[productId] = quantity;
            }
            else
            {
                //cartItems[productId] = quantity;
                cartItems.Add(productId, quantity);
            }
            return PartialView("_CartItemsPartial",cartItems);
        }

        public ActionResult PlaceOrder()
        {
            TempData["amountToPay"] = Session["TotalPrice"];
            return View();
        }
    }
}