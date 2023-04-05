using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using pageadmin.Models;
using static System.Net.WebRequestMethods;
using PagedList;
using PagedList.Mvc;
namespace pageadmin.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        FastfoodEntities5 categori = new FastfoodEntities5();
        public ActionResult LOAISP(int ?page, string search="")
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            List<Categorie> loaisanpham = categori.Categories.Where(n => n.Name.Contains(search)).ToList();
            return View(loaisanpham.OrderBy(n => n.Name).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Addloaisp()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Addloaisp(Categorie loaisp, HttpPostedFileBase file)
        {
            
            if (file == null)
            {
                ViewBag.Thongbao = "Vui long chon anh bia";
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/tailieu"), filename);

                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hinh anh da ton tai";
                    else
                    {
                        file.SaveAs(path);
                    }


                    loaisp.ImageUrl = filename;
                    loaisp.CategoryId = Convert.ToInt32(Request.Form["CategoryId"]);
                    categori.Categories.Add(loaisp);
                    categori.SaveChanges();

                }
                return RedirectToAction("LOAISP");
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            Categorie loaisp = categori.Categories.Find(id);
            return View(loaisp);

        }
        [HttpPost]
        public ActionResult Edit(Categorie loaisp, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.Thongbao = "Vui long chon anh bia";
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/tailieu"), filename);

                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hinh anh da ton tai";
                    else
                    {
                        file.SaveAs(path);
                    }
                    loaisp.ImageUrl = filename;
                    loaisp.CategoryId = Convert.ToInt32(Request.Form["CategoryId"]);
                    Categorie upc = categori.Categories.FirstOrDefault(x => x.CategoryId == loaisp.CategoryId);
                    var updatecate = categori.Categories.Find(loaisp.CategoryId);
                    updatecate.Name = loaisp.Name;
                    updatecate.ImageUrl = loaisp.ImageUrl;
                    updatecate.ISActive = loaisp.ISActive;
                    updatecate.CreatedDate = loaisp.CreatedDate;
                    categori.SaveChanges();

                }
                return RedirectToAction("LOAISP");
            }

        }
        [HttpGet]
        public ActionResult Delete()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {


            var updatecate = categori.Categories.Find(id);
            categori.Categories.Remove(updatecate);
            categori.SaveChanges();
            return RedirectToAction("LOAISP");

        }
        public ActionResult Details(int id)
        {

            List<Categorie> loaisanpham = categori.Categories.ToList();
            Categorie thongtin = new Models.Categorie();
            foreach (Categorie loai in loaisanpham)
            {
                if (loai.CategoryId == id)
                {
                    thongtin = loai;
                    break;
                }
            }
            if (thongtin == null)
            {
                return HttpNotFound();
            }
            return View(thongtin);
        }
    }
}