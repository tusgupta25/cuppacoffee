﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuppaCoffee;

namespace CuppaCoffee.Controllers
{
    public class HomeController : Controller
    {
        //method that redirects to the home page after the error page
        [HandleError]
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //method to add ordered items to cart in the same session
        public ActionResult AddToCart(CuppaCoffee.Order order)
        {
            if (Request.HttpMethod == "POST")
            {
                if (Session["LoggedUserID"] == null)
                {
                    return RedirectToAction("Checkout");
                }
                else
                {
                    if (Session["order_items"] == null || (int)Session["order_items"] == 0)
                        Session["order_items"] = 0;

                    Session["order_items"] = (int)Session["order_items"] + 1;
                    int items = (int)Session["order_items"];

                    Session["order__" + items + "__product_name"] = Request.Form["product_name"];
                    Session["order__" + items + "__roast"] = Request.Form["roast"];
                    Session["order__" + items + "__milk"] = Request.Form["milk"];
                    Session["order__" + items + "__flavor"] = Request.Form["flavor"];
                    Session["order__" + items + "__drink_size"] = Request.Form["drink_size"];
                }
            }
            ViewBag.Orders = new List<Order>();
            ViewBag.Orders.Add(order);
            return RedirectToAction("Checkout");
        }
        //method to chekout items from the cart and update the database
        public ActionResult Checkout()
        {
            if (Request.HttpMethod == "POST")
            {
                CuppaDBEntities dc = new CuppaDBEntities();
                int items = (int)Session["order_items"];
                String uuid = Guid.NewGuid().ToString();
                if (items > 0)
                {
                    String email = (String)Session["LoggedUserID"];
                    for (int i = 1; i <= items; i++)
                    {
                        String pname = (String)Session["order__" + i + "__product_name"];
                        String roast = (String)Session["order__" + i + "__roast"];
                        String milk = (String)Session["order__" + i + "__milk"];
                        String flavor = (String)Session["order__" + i + "__flavor"];
                        String dsize = (String)Session["order__" + i + "__drink_size"];

                        var query = "INSERT INTO dbo.\"Order\" (product_name, roast, milk, flavor, drink_size, order_date, customer_email, uuid) VALUES ('"+pname+"', '"+roast+"', '"+milk+"', '"+flavor+"', '"+dsize+"', GETDATE(), '"+email+"', '"+uuid+"')";
                        dc.Database.ExecuteSqlCommand(query);
                        var query1 = "UPDATE dbo.customers SET rewards = rewards + 5 WHERE customer_email = '" + email + "';";
                        dc.Database.ExecuteSqlCommand(query1);
                    }
                }
                return Redirect("https://paypal.com");
            }
            if (this.Session.Count>0)
            {

                return View("CheckoutPage");

            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Select your favorite drink";

            return View();
        }
        //method for the my account that returns the page when called
        public ActionResult UserPage()
        {
            return View();
        }
        //method that takes the user to the login page after logging out
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        //method called upon to view all the orders made (manager's perspective)
        public ActionResult AllOrders()
        {
            return View();
        }
        //method to update inventory
        public ActionResult Inventory()
        {
            CuppaDBEntities dc = new CuppaDBEntities();
            var prods = dc.products;
            if (Request.HttpMethod == "POST")
            {
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("invproduct_"))
                    {
                        string[] data = key.Split('_');
                        var query = "UPDATE products set product_quantity = " + Request.Form[key] + " WHERE product_ID = " + data[1] + ";";
                        int noOfRowUpdated = dc.Database.ExecuteSqlCommand(query);
                    }
                }
                ViewBag.Message = "Successfully Updated";
            }
 
            return View();
        }
        //method that creates a session for a user and distiguished between customer and manager
        public ActionResult Login(CuppaCoffee.customer c)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (CuppaDBEntities dc = new CuppaDBEntities())
                {
                    if (Request.Form["Log In"] != null)
                    {
                        var v = dc.customers.Where(a => a.customer_email.Equals(c.customer_email) && a.customer_password.Equals(c.customer_password)).FirstOrDefault();
                        if (v != null)
                        {
                            Session["LoggedUserID"] = v.customer_email.ToString();
                            Session["order_items"] = 0;
                            Session["LoggedUserFirstname"] = v.customer_firstname.ToString();
                            Session["LoggedUserLastName"] = v.customer_lastname.ToString();
                            Session["isManager"] = v.IsManager;
                            if (v.Rewards != null)
                            {
                                Session["LoggedUserRewards"] = v.Rewards.ToString();
                            }
                            else
                            {
                                Session["LoggedUserRewards"] = 0;
                            }
                            
                            return RedirectToAction("UserPage");
                        }
                    }
                    if (Request.Form["Register"] != null)
                    {
                        
                        //you should check duplicate registration here 
                        dc.customers.Add(c);
                        dc.SaveChanges();
                        ModelState.Clear();
                        c = null;
                        ViewBag.Message = "Successfully Registered";
                        return View(c);
                    }
                }
            }
            return View(c);
        }
    }
}
