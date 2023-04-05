using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pageadmin.Models;

namespace pageadmin.Controllers
{
  
    public class SPCartController : Controller
    {
        // GET: SPCart
        FastfoodEntities5 data = new FastfoodEntities5();
        public CartSP GetCart()
        {
            CartSP cart = Session["CartSP"] as CartSP;
            if(cart == null || Session["CartSP"] == null)
            {
                cart = new CartSP();
                Session["CartSP"] = cart;
            }
            return cart;
        }
       public ActionResult AddtoCart(int id)
        {
            var pro = data.Products.SingleOrDefault(s => s.ProductId == id);
            if(pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowtoCart", "SPCart");
        }
        public ActionResult ShowtoCart()
        {
            if (Session["CartSP"] == null)
            return RedirectToAction("ShowtoCart", "SPCart");
            CartSP cart = Session["CartSP"] as CartSP;
            return View(cart);
        }
        public ActionResult update_quantity_product(FormCollection form)
        {
            CartSP cart = Session["CartSP"] as CartSP;
            int id_product = int.Parse(form["id_ProductId"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.update_quantity(id_product, quantity);
            return RedirectToAction("ShowtoCart", "SPCart");
        }
        public ActionResult RemoveCartProduct(int id)
        {
            CartSP cart = Session["CartSP"] as CartSP;
            cart.Remove_Cart(id);
            return RedirectToAction("ShowtoCart", "SPCart");
        }
       
        

    }
}