using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NeroMerch.DATA;

namespace NeroMerch.Manager
{
    public static class OrderManager
    {
        public static int PlaceOrder(string email, CartManager cart, BillingAddress address)
        {
            //User laden, der bestellt hat
            var dbUser = CustomerManager.GetCustomer(email);
            //Produkte laden, die bestellt werden sollen
            var dbProducts = cart.GetProducts();

            //Order anlegen
            var dbOrder = new Order();
            dbOrder.DateOrdered = DateTime.Now;
            dbOrder.DatePaid = null;
            dbOrder.PriceTotal = cart.GetTotal();
            dbOrder.PriceNet = dbOrder.PriceTotal / 120 * 100;
            dbOrder.CustomerId = dbUser.Id;

            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                db.Order.Add(dbOrder);
                db.SaveChanges();
            }

            //BillingAddress anlegen
            address.OrderId = dbOrder.Id;
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                db.BillingAddress.Add(address);
                db.SaveChanges();
            }

            //Orderlines anlegen
            var dbOrderLines = new List<OrderLine>();
            foreach (var p in dbProducts)
            {
                var dbOrderLine = new OrderLine();
                dbOrderLine.OrderId = dbOrder.Id;
                dbOrderLine.ProductId = p.Id;
                dbOrderLine.Amount = cart.Cart[p.Id];
                dbOrderLine.NetUnitPrice = p.NetUnitPrice;

                dbOrderLine.LinePrice = p.NetUnitPrice * dbOrderLine.Amount;

                dbOrderLines.Add(dbOrderLine);

            }
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                db.OrderLine.AddRange(dbOrderLines);
                db.SaveChanges();
            }

            return dbOrder.Id;
        }

        public static bool IsCardNumberValid(string cc)
        {
            if (cc.Length < 12 || cc.Length > 19)
            {
                return false;
            }
            int totalSum = 0;
            bool odd = false;
            for (int i = cc.Length - 1; i >= 0; i--)
            {
                int num = int.Parse(cc[i].ToString());
                if (odd)
                {
                    num *= 2;
                    if (num > 9)
                        num -= 9;
                    totalSum += num;
                }
                else
                {
                    totalSum += num;
                }
                odd = !odd;
            }
            return totalSum % 10 == 0;
        }

        public static bool MarkOrderAsPaid(int orderId)
        {
            //Bestellug als bezahlt markieren
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                var dbOrder = db.Order.Where(o => o.Id == orderId).SingleOrDefault();
                if (dbOrder == null)
                {
                    return false;
                }
                dbOrder.DatePaid = DateTime.Now;

                //"Erzeugen" der Lizenzschlüssel

                var dbNewLicenseKeys = new List<LicenseKey>();

                //Abrufen der OrderLines zu der Bestellung
                var dbOrderLines = db.OrderLine.Where(ol => ol.OrderId == orderId).ToList();

                foreach (var dbOrderLine in dbOrderLines)
                {
                    for (int i = 0; i < dbOrderLine.Amount; i++)
                    {
                        var newLicenseKey = new LicenseKey();
                        newLicenseKey.KeyString = Guid.NewGuid().ToString();
                        newLicenseKey.WasUsed = false;
                        newLicenseKey.OrderLine_Id = dbOrderLine.Id;

                        dbNewLicenseKeys.Add(newLicenseKey);
                    }
                }

                db.LicenseKey.AddRange(dbNewLicenseKeys);

                db.SaveChanges();
            }
            return true;
        }

        public static List<Order> LoadOrdersFromUser(string email)
        {
            var resultList = new List<Order>();

            var dbUser = CustomerManager.GetCustomer(email);

            if (dbUser == null) return resultList;

            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.Order.Where(o => o.CustomerId == dbUser.Id).ToList();
            }
        }

        public static Order GetOrderDetail(int oid)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.Order.Include(o => o.BillingAddress).Where(o => o.Id == oid).SingleOrDefault();
            }
        }

        public static List<OrderContent> GetOrderContents(int oid)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.OrderContent.Where(oc => oc.OrderId == oid).AsNoTracking().ToList();
            }
        }

        public static void ActivateKey(int keyId)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                var key = db.LicenseKey.Where(k => k.Id == keyId).SingleOrDefault();
                if (key != null) { key.WasUsed = true; }
                db.SaveChanges();
            }
        }
        public static List<OrderLine> GetFullOrderLines(int oid)
        {
            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.OrderLine.Include(ol => ol.Product).Where(ol => ol.Order.Id == oid).ToList();
            }
        }
    }
}