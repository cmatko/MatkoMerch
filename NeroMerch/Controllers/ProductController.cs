using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NeroMerch.DATA;
using NeroMerch.Manager;
using NeroMerch.Models;

namespace NeroMerch.Controllers
{
    public class ProductController : Controller
    {
        private IN31_NeroMerch_WebshopEntities db = new IN31_NeroMerch_WebshopEntities();

        // GET: ProductsSSS/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: ProductsSSS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,NetUnitPrice,ImagePath,Description,ManufacturerId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Overview");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: ProductsAAAA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // POST: ProductsAAAA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,NetUnitPrice,ImagePath,Description,ManufacturerId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Overview");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturer, "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? prodId)
        {
            if (prodId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(prodId);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int prodId)
        {
            Product product = db.Product.Find(prodId);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Overview");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Overview(string searchText = "", int catId = -1, int manId = -1)
        {
            //Daten aus DB abrufen
            var dbProducts = ProductManager.GetAllProducts(searchText, catId, manId);            
            var dbCategories = ProductManager.GetAllCategories();
            var dbManufactures = ProductManager.GetAllManufactures();

            //Viewmodel erstellen
            var vm = new OverviewVm();
            vm.SearchText = searchText;
            vm.SelectedCategory = catId;
            vm.SelectedManufacture = manId;

            

            //Kategorien ins Viewmodel mappen
            foreach (var cat in dbCategories)
            {
                vm.Categories.Add(cat.Id, cat.Name);
            }
            foreach (var man in dbManufactures)
            {
                vm.Manufactures.Add(man.Id, man.Name);
            }

            //Produkte ins Viewmodel mappen
            foreach (var item in dbProducts)
            {
                var vmProduct = new OverviewVmProduct();

                vmProduct.ManufacturerName = item.Manufacturer;
                vmProduct.Id = item.Id;
                vmProduct.ImagePath = item.ImagePath;
                vmProduct.ProductName = item.ProductName;
                vmProduct.Price = item.NetUnitPrice;

                vm.Products.Add(vmProduct);
            }         

            return View(vm); //Viewmodel an die View geben
        }

        [HttpGet]
        public ActionResult Detail(int prodId)
        {
            //Produkt aus DB laden
            var dbProduct = ProductManager.GetProduct(prodId);
            

            //Bewertungen aus DB laden
            var dbRatings = ProductManager.GetRatings(prodId);

            if (dbProduct == null)
            {
                return RedirectToAction("Overview");
            }
           
            //Viewmodel erstellen
            var vm = new DetailVm();

            //DB-Daten in Viewmodel mappen
            vm.Category = dbProduct.Category;
            vm.Manufacturer = dbProduct.Manufacturer;
            vm.Description = dbProduct.Description;
            vm.Id = dbProduct.Id;
            vm.ImagePath = dbProduct.ImagePath;
            vm.Name = dbProduct.ProductName;
            vm.Price = dbProduct.NetUnitPrice;

            if (dbRatings.Count > 0)
            {
                vm.AvgRating = dbRatings.Average(r => r.Value);
            }
            else
            {
                vm.AvgRating = 0;
            }

            vm.NumRatings = dbRatings.Count;

            foreach (var rating in dbRatings)
            {
                if (!string.IsNullOrWhiteSpace(rating.Comment))
                {
                    vm.Comments.Add(rating.Comment);
                }
            }

            //Feststellen, ob Benutzer bewerten darf oder nicht
            if (User.Identity.IsAuthenticated)
            {
                vm.CanRate = ProductManager.CanCustomerRateProduct(prodId, User.Identity.Name);
            }
            else
            {
                vm.CanRate = false;
            }

            //Viewmodel an View übergeben
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult NewRating(byte rating, string comment, int prodId)
        {
            //todo: Prüfen ob comment nicht zu lang ist

            ProductManager.NewRating(prodId, rating, comment, User.Identity.Name);

            //todo: Prüfen bewertung eh eingetragen wurde

            TempData["ConfirmMessage"] = "Bewertung erfolgreich abgegeben";

            return RedirectToAction("Detail", new { prodId });
        }
    }
}