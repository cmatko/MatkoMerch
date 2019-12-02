using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NeroMerch.DATA;

namespace NeroMerch.Manager
{
    public static class ProductManager
    {
        public static List<ProductInfo> GetAllProducts()
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.ProductInfo.ToList();
            }
        }
       
        public static List<ProductInfo> GetAllProducts(string searchText, int catId, int manId)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                var dbProducts1 = db.SearchAndFilter(searchText, catId, manId).ToList();
            

                var productInfos = new List<ProductInfo>();

                foreach (var dbProduct in dbProducts1)
                {
                    var prodInfo1 = new ProductInfo();

                    prodInfo1.Category = dbProduct.Category;
                    prodInfo1.CategoryId = dbProduct.CategoryId;
                    prodInfo1.Manufacturer = dbProduct.Manufacturer;
                    prodInfo1.ManufacturerId = dbProduct.ManufacturerId;
                    prodInfo1.Description = dbProduct.Description;
                    prodInfo1.Id = dbProduct.Id;
                    prodInfo1.ImagePath = dbProduct.ImagePath;
                    prodInfo1.ProductName = dbProduct.ProductName;
                    prodInfo1.NetUnitPrice = dbProduct.NetUnitPrice;

                    productInfos.Add(prodInfo1);
                }
               
                return productInfos;
            }
        }

        public static ProductInfo GetProduct(int id)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                //todo mögliche Exception von Single abfangen
                return db.ProductInfo.Where(p => p.Id == id).SingleOrDefault();
            }
        }
        

        public static List<Rating> GetRatings(int prodId)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.Rating.Where(r => r.ProductId == prodId).ToList();
            }
        }

        //public static List<Category> GetAllCategories()
        //{
        //    using (var db = new SoftwareShopEntities())
        //    {
        //        return db.Category.ToList();
        //    }
        //}
        
        public static List<GetAllCategories_Result> GetAllCategories()
        {
            //Stored Procedure Aufrufen
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.GetAllCategories().ToList();
            }
        }
        public static List<GetAllManuefactures_Result> GetAllManufactures()
        {
            //Stored Procedure Aufrufen
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.GetAllManuefactures().ToList();
            }
        }


        public static bool CanCustomerRateProduct(int prodId, string email)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                //UserId laden
                var userId = CustomerManager.GetCustomer(email).Id;

                //Ermitteln, ob der Benutzer das Produkt schon bestellt hat
                var allOrders = db.Order.Where(o => o.CustomerId == userId).Include(o => o.OrderLine).ToList();

                bool hasOrderedProduct = false;

                foreach (var order in allOrders)
                {
                    hasOrderedProduct = order.OrderLine.Any(ol => ol.ProductId == prodId);
                    if (hasOrderedProduct)
                    {
                        break;
                    }
                }

                //Falls nicht, darf er nicht bewerten
                if (!hasOrderedProduct)
                {
                    return false;
                }

                //Falls schon, muss geprüft werden ob er schon einmal bewertet hat
                var hasAlreadyRated = db.Rating.Any(r => r.ProductId == prodId && r.CustomerId == userId);

                if (hasAlreadyRated)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static void NewRating(int prodId, byte rating, string comment, string email)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                Rating r = new Rating();
                r.Comment = comment;
                r.ProductId = prodId;
                r.CustomerId = CustomerManager.GetCustomer(email).Id;
                r.Value = rating;

                db.Rating.Add(r);
                db.SaveChanges();
            }
        }
    }   
}