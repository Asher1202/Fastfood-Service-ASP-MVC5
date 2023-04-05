using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pageadmin.Models;
namespace pageadmin.Controllers
{
    public class LoginController : Controller
    {
        FastfoodEntities3 _db = new FastfoodEntities3();
        // GET: Login
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(User _user)
        {
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email) && s.Password.Equals(_user.Password)).FirstOrDefault();
            if(check == null)
            {
                _user.Email = "Email hoặc username hoặc mật khẩu sai";
                return View("DangNhap", _user);
            }
            else
            {
                Session["UserId"] = _user.UserId;
                Session["Username"] = _user.Name;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult DangKy() { 
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(User _user)
        {
            if(ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(e => e.Email== _user.Email);
                if(check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("thucDon","Home");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
                
            }
            return View();
        }
    }
}