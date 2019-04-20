using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuppaCoffee;

namespace CuppaCoffee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AddToCart(CuppaCoffee.Order order)
        {
            ViewBag.Orders = new List<Order>();
            ViewBag.Orders.Add(order);
            return RedirectToAction("Checkout");
        }

        public ActionResult Checkout()
        {
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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

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
                            Session["LoggedUserFirstname"] = v.customer_firstname.ToString();
                            Session["LoggedUserLastName"] = v.customer_lastname.ToString();
                            Session["isManager"] = v.IsManager;
                            if (v.customer_phonenumber != null)
                            {
                                Session["LogedUserPhoneNumber"] = v.customer_phonenumber.ToString();
                            }
                            else
                            {
                                Session["LoggedUserPhoneNumber"] = "N/A".ToString();
                            }
                            if (v.customer_DOB != null)
                            {
                                Session["LoggedUserDOB"] = v.customer_DOB.ToString();
                            }
                            else
                            {
                                Session["LoggedUserDOB"] = "N/A".ToString();
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
