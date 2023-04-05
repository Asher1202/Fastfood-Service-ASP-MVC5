using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pageadmin.Models;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Data.Entity.Core.Metadata.Edm;
using PagedList;
using PagedList.Mvc;
using System.Web.Razor.Tokenizer.Symbols;

namespace pageadmin.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        FastfoodEntities5 data = new FastfoodEntities5();
        public ActionResult SanPham(int ?page, string search="")
        {
           
            
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            List<Product> sanpham = data.Products.Where(n => n.Name.Contains(search)).ToList();
            return View(sanpham.OrderBy(n => n.Name).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult AddSP()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddSP( Product model, HttpPostedFileBase file)
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
                   //ViewBag.CategoryId = new SelectList(data.Categories.ToList().OrderBy(m => m.Name), "CategoryId", "Name");
                    model.ImageUrl = filename;
                    model.CategoryId = Convert.ToInt32(Request.Form["CategoryId"]);
                    data.Products.Add(model);
                    data.SaveChanges();
                
                }
                return RedirectToAction("SanPham");
            }


        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            Product model = data.Products.Find(id);
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(Product model,HttpPostedFileBase file)
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


                    model.ImageUrl = filename;
                    model.CategoryId = Convert.ToInt32(Request.Form["CategoryId"]);
                    Product upd = data.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
                    model.CategoryId = Convert.ToInt32(Request.Form["CategoryId"]);
                    var updatemodel = data.Products.Find(model.ProductId);
                    updatemodel.Name = model.Name;
                    updatemodel.Description = model.Description;
                    updatemodel.Price = model.Price;
                    updatemodel.Quantity = model.Quantity;
                    updatemodel.ImageUrl = model.ImageUrl;
                    updatemodel.CategoryId = model.CategoryId;
                    updatemodel.IsActive = model.IsActive;
                    updatemodel.CreatedDate = model.CreatedDate;
                    data.SaveChanges();

                }
               
                return RedirectToAction("SanPham");
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

            
            var updatemodel = data.Products.Find(id);
            data.Products.Remove(updatemodel);
            data.SaveChanges();
            return RedirectToAction("SanPham");

        }
        public ActionResult Details(int id)
        {
           
            List<Product> sanpham = data.Products.ToList();
            Product thongtin = new Models.Product();
            foreach (Product sp in sanpham)
            {
                if (sp.ProductId == id)
                {
                    thongtin = sp;
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