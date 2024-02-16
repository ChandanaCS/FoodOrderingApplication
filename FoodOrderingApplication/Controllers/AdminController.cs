using FoodOrdering_DataAccessLayer_Library;
using FoodOrderingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
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

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            int productId = id;
            AdminDAL dal = new AdminDAL();
            ProductList productList = new ProductList();
            productList = dal.FindProduct(productId);
            ProductModel model = new ProductModel();
            model.ProductID = productList.ProductID;
            model.ProductName = productList.ProductName;
            model.Price = productList.Price;
            return View(model);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                AdminDAL dal = new AdminDAL();
                ProductList productList = new ProductList();
                productList.ProductName = collection["ProductName"];
                productList.Price = Convert.ToDecimal(collection["Price"]);
                bool completed = dal.AddNewProduct(productList);
                if (completed)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.NotAbleToCreate = Content("Something went wrong.. try again..!!");
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            int productId = id;
            AdminDAL dal = new AdminDAL();
            ProductList productList = new ProductList();
            productList = dal.FindProduct(productId);
            ProductModel model = new ProductModel();
            model.ProductID = productList.ProductID;
            model.ProductName = productList.ProductName;
            model.Price = productList.Price;
            return View(model);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                AdminDAL dal = new AdminDAL();
                ProductList productList = new ProductList();
                productList.ProductID = id;
                productList.ProductName = collection["ProductName"];
                productList.Price = Convert.ToDecimal(collection["Price"]);
                bool completed = dal.EditProduct(productList,id);
                if(completed)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.NotAbleToEdit = Content("Something went wrong.. try again..!!");
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            int productId = id;
            AdminDAL dal = new AdminDAL();
            ProductList productList = new ProductList();
            productList = dal.FindProduct(productId);
            ProductModel model = new ProductModel();
            model.ProductID = productList.ProductID;
            model.ProductName = productList.ProductName;
            model.Price = productList.Price;
            return View(model);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                AdminDAL dal = new AdminDAL();
                int productId = id;
                bool completed = dal.RemoveProduct(productId);
                if (completed)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.NotAbleToEdit = Content("Something went wrong.. try again..!!");
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
    }
}
