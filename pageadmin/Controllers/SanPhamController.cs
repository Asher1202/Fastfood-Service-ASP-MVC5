using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using pageadmin.Models;
using PagedList;

namespace pageadmin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        FastfoodEntities3 data = new FastfoodEntities3();
       public ActionResult Product(int? page, string search = "")
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            List<Product> products = data.Products.Where(n => n.Name.Contains(search)).ToList();
            return View(products.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));
        }
    }
}