using pageadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageadmin.Areas.Admin.Controllers
{
    public class NhanvienController : Controller
    {
        // GET: Admin/Nhanvien
        public ActionResult Phanloai()
        {
            FastfoodEntities5 db = new FastfoodEntities5();
            Nhanvien nvsession = (Nhanvien)Session["User"];
            var count = db.Phanquyens.Count(m => m.IdNhanvien == nvsession.ID && m.IdChucnang == 1);
            if (count == 0)
            {
                return Redirect("/Admin/Baoloi/Khongcoquyen");
            }
            return View();
        }
        public ActionResult Sanpham()
        {
            FastfoodEntities5 db = new FastfoodEntities5();
            Nhanvien nvsession = (Nhanvien)Session["User"];
            var count = db.Phanquyens.Count(m => m.IdNhanvien == nvsession.ID && m.IdChucnang == 2);
            if (count == 0)
            {
                return Redirect("/Admin/Baoloi/Khongcoquyen");
            }

            return View();
        }
        
    }
}