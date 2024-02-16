using FoodOrdering_DataAccessLayer_Library;
using FoodOrderingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                CustomerDAL dal = new CustomerDAL();
                Customer custdal = new Customer
                {
                    CustomerEmail = collection["CustomerEmail"],
                    Password = collection["Password"]
                };
                if (dal.IsLoginCredentialsValid(custdal))
                {
                    return RedirectToAction("Index","Customer");
                }
                else
                {
                    return Content("Email or Password is Incorrect");
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                CustomerDAL dal = new CustomerDAL();
                Customer custdal = new Customer();
                custdal.CustomerEmail = collection["CustomerEmail"];
                custdal.Password = collection["Password"];

                if (dal.RegisterNewUser(custdal))
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    Content("Something went wrong! Try Again..");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(FormCollection collection)
        {
            try
            {
                AdminDAL dal = new AdminDAL();
                Admin admindal = new Admin
                {
                    AdminEmail = collection["AdminEmail"],
                    Password = collection["Password"]
                };
                if (dal.IsLoginCredentialsValidForAdmin(admindal))
                {
                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    ViewBag.LoginFailedMsg = "Email or Password is Incorrect";
                    return View();

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
    }
}