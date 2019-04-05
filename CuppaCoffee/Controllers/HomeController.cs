using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }
        
       
        public ActionResult Login(MVCLogin.User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (CuppaDBEntities dc = new CuppaDBEntities())
                {
                    var v = dc.customers.Where(a => a.customer_email.Equals(u.customer_email) && a.customer_password.Equals(u.customer_password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.customer_email.ToString();
                        Session["LogedUserFullname"] = v.customer_firstname.ToString();
                        return RedirectToAction("UserPage");
                    }
                }
            }
            return View(u);
        }
    }
}
