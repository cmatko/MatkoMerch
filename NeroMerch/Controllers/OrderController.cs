using NeroMerch.Manager;
using NeroMerch.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeroMerch.DATA;
using Rotativa;

namespace NeroMerch.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult New()
        {
            var cartManager = Session.GetCart();
            var dbProducts = cartManager.GetProducts();

            var vm = new NewVm();

            foreach (var p in dbProducts)
            {
                var vmProduct = new NewVmProduct();
                vmProduct.ManufacturerName = p.Manufacturer;
                vmProduct.ProductName = p.ProductName;
                vmProduct.Amount = cartManager.Cart[p.Id];
                vmProduct.LinePrice = p.NetUnitPrice * vmProduct.Amount;

                vm.Products.Add(vmProduct);
            }
            vm.TotalPrice = vm.Products.Sum(p => p.LinePrice);

            var dbCustomer = CustomerManager.GetCustomer(User.Identity.Name);

            vm.City = dbCustomer.City;
            vm.FirstName = dbCustomer.FirstName;
            vm.LastName = dbCustomer.LastName;
            vm.Street = dbCustomer.Street;
            vm.Title = dbCustomer.Title;
            vm.Zip = dbCustomer.Zip;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Place(NewVm vm)
        {
            var cart = Session.GetCart();

            //Viewmodel auf Datenbankmodel mappen
            BillingAddress addr = new BillingAddress();
            addr.City = vm.City;
            addr.FirstName = vm.FirstName;
            addr.LastName = vm.LastName;
            addr.Street = vm.Street;
            addr.Title = vm.Title;
            addr.ZipCode = vm.Zip;

            //Bestellung in DB speichern
            var orderId = OrderManager.PlaceOrder(User.Identity.Name, cart, addr);

            //Warenkorb leeren
            cart.ClearCart();
            Session.SaveCart(cart);
            //Für die Bestellung bedanken

            string hostname = HttpContext.Request.Url.Authority;
            string subject = $"Ihre NeroMerch Bestellung # {orderId} vom {DateTime.Now.ToShortDateString()}";
            string body =
                $"<h1>Vielen Dank für Ihre Bestellung!</h1>" +
                $"<h3>Sie können den Status Ihrer Bestellung <a href='http://{hostname}/Customer/MyOrders'>NeroMaster</a> prüfen.</h3>" +
                $"<hr>Außerdem haben wir Ihnen Ihre Rechnung im PDF Format an diese E-Mail angehängt!>" +
                $"<h4>Vergessen Sie nicht Ihre bestellten Produkte zu bewerten!</h4>";

            foreach (var product in cart.GetProducts())
            {
                body += $"<p><a href='http://{hostname}/Product/Detail?prodId={product.Id}'>{product.ProductName} bewerten</a></p>";
            }

            body += $"<h3>Wir freuen uns auf ein Wiedersehen,<br><br>" +
            $"Star Wars NeroMerch</h3>";

           

            //string to = User.Identity.Name;
            string to = "nenad.djurdjevic@qualifizierung.at";

            var pdfAction = new ActionAsPdf("Receipt", new { oid = orderId });

            var pdfBytes = pdfAction.BuildPdf(this.ControllerContext);

            EmailManager.SendMailWithPdf(to, body, subject, pdfBytes);

            //Wenn Überweisung ausgewählt...
            //weiter zur Danke Seite

            //Wenn Kreditkarte ausgewählt...
            //weiter zur Eingabe der Kartendaten
            switch (vm.PaymentMethod)
            {
                //Kreditkarte
                case 0:
                    {
                        TempData["ConfirmMessage"] = "Ihre Bestellung wurde aufgenommen." +
                            "Bitte geben Sie Ihre Daten ein um die Bestellung zu bezahlen.";
                        return View("EnterCard", new EnterCardVm()
                        {
                            OrderId = orderId
                        });
                    }

                //Überweisung
                case 1:
                    {
                        TempData["OrderData"] = new ThankYouVm()
                        {
                            OrderId = orderId,
                            IsPaid = false
                        };
                        return RedirectToAction("ThankYou");
                    }

                default: return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [Authorize]
        public ActionResult CardData(EnterCardVm vm)
        {
            var currDate = DateTime.Now;
            var currMonth = currDate.Month;
            var currYear = currDate.Year;

            //Wenn Daten nicht gültig sind
            if (!ModelState.IsValid)
            {
                //Zurück zur Eingaben der Daten
                TempData["ErrorMessage"] = "Bitte alle Daten eingeben!";
                return View("EnterCard", vm);
            }
            if (!OrderManager.IsCardNumberValid(vm.CardNumber))
            {
                //Ungültige Kartennummer
                TempData["ErrorMessage"] = "Ungültige Kartennummer";
                return View("EnterCard", vm);
            }
            if (vm.Year < currYear)
            {
                //Kann nicht mehr gültig sein
                TempData["ErrorMessage"] = "Karte schon abgelaufen";
                return View("EnterCard", vm);
            }
            else if (vm.Year == currYear)
            {
                if (vm.Month < currMonth)
                {
                    //Kann nicht mehr gültig sein
                    TempData["ErrorMessage"] = "Karte schon abgelaufen";
                    return View("EnterCard", vm);
                }
            }

            //Ansonsten, wenn alles passt
            //Bestellung als Bezahlt markieren (inkludiert das Erzeugen von Lizenzschlüssel)
            //Dann auf die Danke Seite

            OrderManager.MarkOrderAsPaid(vm.OrderId);

            TempData["OrderData"] = new ThankYouVm()
            {
                OrderId = vm.OrderId,
                IsPaid = true
            };
            return RedirectToAction("ThankYou");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ThankYou(/*ThankYouVm vm*/)
        {
            //if(vm == null || vm.OrderId <= 0)
            //{
            var vm = (ThankYouVm)TempData.Peek("OrderData");
            //}

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ThankYou(ThankYouVm vm)
        {
            //if (vm == null || vm.OrderId <= 0)
            //{
            //    vm = (ThankYouVm)TempData.Peek("OrderData");
            //}

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int oid)
        {
            var vm = new DetailsVm();

            var dbOrder = OrderManager.GetOrderDetail(oid);

            vm.BillingCity = dbOrder.BillingAddress.City;
            vm.BillingFirstName = dbOrder.BillingAddress.FirstName;
            vm.BillingLastName = dbOrder.BillingAddress.LastName;
            vm.BillingStreet = dbOrder.BillingAddress.Street;
            vm.BillingTitle = dbOrder.BillingAddress.Title;
            vm.BillingZipCode = dbOrder.BillingAddress.ZipCode;

            vm.IsPaid = dbOrder.DatePaid == null ? false : true;

            vm.OrderDate = dbOrder.DateOrdered;
            vm.OrderId = dbOrder.Id;
            vm.OrderNetTotal = dbOrder.PriceNet;
            vm.OrderTotal = dbOrder.PriceTotal;

            var dbOrderContents = OrderManager.GetOrderContents(oid);

            foreach (var item in dbOrderContents)
            {
                var key = new DetailsVmOrderedKey();
                key.Manufacturer = item.Manufacturer;
                key.Id = item.KeyId;
                key.Key = item.KeyString;
                key.Price = item.NetUnitPrice;
                key.Product = item.ProductName;
                key.UsedKey = item.WasUsed;

                vm.OrderedKeys.Add(key);
            }

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ActivateKey(int oid, int keyId)
        {
            OrderManager.ActivateKey(keyId);

            return RedirectToAction("Details", new { oid });
        }
        [HttpGet]
        public ActionResult Receipt(int oid)
        {
            var vm = new ReceiptVM();

            var dbOrder = OrderManager.GetOrderDetail(oid);

            vm.CustomerCity = dbOrder.BillingAddress.City;
            vm.CustomerFirstname = dbOrder.BillingAddress.FirstName;
            vm.CustomerLastname = dbOrder.BillingAddress.LastName;
            vm.CustomerStreet = dbOrder.BillingAddress.Street;
            vm.CustomerTitle = dbOrder.BillingAddress.Title;
            vm.CustomerZip = dbOrder.BillingAddress.ZipCode;

            vm.OrderDate = dbOrder.DateOrdered;
            vm.OrderId = dbOrder.Id;
            vm.TotalGross = dbOrder.PriceTotal;
            vm.TotalNet = dbOrder.PriceNet;
            vm.VatAmount = dbOrder.PriceTotal - dbOrder.PriceNet;
            vm.VatPercent = 20.0;

            var dbOrderLines = OrderManager.GetFullOrderLines(oid);

            foreach (var ol in dbOrderLines)
            {
                var prod = new ReceiptVMProduct();

                prod.Amount = ol.Amount;
                prod.LinePrice = ol.LinePrice ?? throw new Exception("Error");
                prod.Name = ol.Product.ProductName;

                vm.Products.Add(prod);
            }
            return View(vm);
        }
        [HttpGet]
        [Authorize]
        public ActionResult ReceiptPdf(int oid)
        {
            var pdf = new ActionAsPdf("Receipt", new { oid });
            return pdf;
        }
    }

}