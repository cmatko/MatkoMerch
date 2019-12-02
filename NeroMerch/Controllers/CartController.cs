using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeroMerch.Manager;
using NeroMerch.Models;

namespace NeroMerch.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Show()
        {
            //Warenkorb aus Session holen
            var mgr = Session.GetCart();

            //Daten aus Datenbank laden
            var dbProducts = mgr.GetProducts();

            //In Viewmodel mappen
            var vm = new ShowVm();

            foreach (var dbProduct in dbProducts)
            {
                var vmProduct = new ShowVmProduct();
                vmProduct.CompanyName = dbProduct.Manufacturer;
                vmProduct.Id = dbProduct.Id;
                vmProduct.ImagePath = dbProduct.ImagePath;
                vmProduct.Name = dbProduct.ProductName;
                vmProduct.UnitPrice = dbProduct.NetUnitPrice;

                vmProduct.Quantity = mgr.Cart[dbProduct.Id]; //todo in Cart-Methode kapseln
                vmProduct.LinePrice = dbProduct.NetUnitPrice * vmProduct.Quantity; //todo in Cart-Methode kapseln

                vm.Products.Add(vmProduct);
            }

            vm.TotalPrice = vm.Products.Sum(p => p.LinePrice);

            //Viewmodel an View übergeben
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeQty(int prodId, int qty, bool? info)
        {
            if (info == null)
            {
                var mgr = Session.GetCart();
                mgr.Remove(prodId);
                Session.SaveCart(mgr);
            }
            else if (info == true)
            {
                Add(prodId, qty);
            }
            else
            {
                Remove(prodId, qty);
            }

            return RedirectToAction("Show");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(int prodId, int qty)
        {
            var mgr = Session.GetCart();

            mgr.Add(prodId, qty);

            Session.SaveCart(mgr);

            return RedirectToAction("Overview", "Product");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Remove(int prodId, int qty)
        {
            var mgr = Session.GetCart();

            mgr.Remove(prodId, qty);

            Session.SaveCart(mgr);

            return RedirectToAction("Overview", "Product");
        }
    }

    public static class SessionExtensions
    {
        public static CartManager GetCart(this HttpSessionStateBase session)
        {
            return new CartManager((Dictionary<int, int>)session["CART"]);
        }

        public static void SaveCart(this HttpSessionStateBase session, CartManager mgr)
        {
            session["CART"] = mgr.Cart;
        }
    }
}