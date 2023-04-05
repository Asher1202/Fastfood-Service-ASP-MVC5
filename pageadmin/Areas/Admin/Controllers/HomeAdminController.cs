using pageadmin.Models;
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


            FastfoodEntities5 db = new FastfoodEntities5();
            var nhanvien = db.Nhanviens.SingleOrDefault(m => m.Username.ToLower() == user.ToLower() && m.Password == password);


            if (nhanvien != null)
            {
                Session["user"] = nhanvien;

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