using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreIRMA.WebSite.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult CRODashBoard()
        {
            if (Session["UserLogon"] != null)
            {
                return View();
            }
            else
                return Redirect("~/Account/Login");
        }

        public ActionResult AdminDashBoard()
        {
            if (Session["UserLogon"] != null)
            {
                return View();
            }
            else
                return Redirect("~/Account/Login");
        }
    }
}
