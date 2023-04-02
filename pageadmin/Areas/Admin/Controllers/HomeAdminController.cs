using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace pageadmin.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }


        }
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(string user, string password)
        {


            if (user.ToLower() == "admin" && password == "123")
            {
                Session["user"] = "admin";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Tài khoản không đúng";
                return View();
            }


        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
    }
}