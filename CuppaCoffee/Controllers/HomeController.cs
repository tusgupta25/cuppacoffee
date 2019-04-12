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
        public ActionResult Login(CuppaCoffee.customer c)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (CuppaDBEntities dc = new CuppaDBEntities())
                {
                    var v = dc.customers.Where(a => a.customer_email.Equals(c.customer_email) && a.customer_password.Equals(c.customer_password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LoggedUserID"] = v.customer_email.ToString();
                        Session["LoggedUserFirstname"] = v.customer_firstname.ToString();
                        Session["LoggedUserLastName"] = v.customer_lastname.ToString();
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
            }
            return View(c);
        }
    }
}
