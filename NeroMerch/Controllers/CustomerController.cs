using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeroMerch.DATA;
using NeroMerch.Manager;
using NeroMerch.Models;
using System.Web.Security;

namespace NeroMerch.Controllers
{
   
    public class CustomerController : Controller
    {
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterVM vmReg)
        {
            if (!vmReg.AcceptedTerms)
            {
                //Fehlermeldung ausgeben
                return View();
            }

            if (vmReg.Passwort != vmReg.Passwort_bestätigen)
            {
                //Fehlermeldung ausgeben
                return View();
            }

            //Mappen von Viewmodel auf Datenbankmodel (Entitätsmodel)
            var newCustomer = new Customer();

            newCustomer.City = vmReg.City;
            newCustomer.Email = vmReg.Email;
            newCustomer.FirstName = vmReg.FirstName;
            newCustomer.LastName = vmReg.LastName;
            newCustomer.Street = vmReg.Street;
            newCustomer.Title = vmReg.Title;
            newCustomer.Zip = vmReg.Zip;

            //newUser.Id = ; //Wird von DB zugewiesen
            //newUser.PasswordHash = ; //Wird von Logic gemacht
            //newUser.Salt = ; //Wird von Logic gemacht

            CustomerManager.RegisterCustomer(newCustomer, vmReg.Passwort);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
                    
        [HttpPost]
        public ActionResult Login(LoginVM vmLogin)

        {
            var canLogin = CustomerManager.IsLoginValid(vmLogin.Email, vmLogin.Password);

            if (!canLogin)
            {
                return View();
            }

            //Benutzer als eingeloggt markieren
            LogUserIn(vmLogin.Email);


            return RedirectToAction("Index", "Home");
        }
       
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private void LogUserIn(string email)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                0,
                email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                ""
                );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Response.Cookies.Add(authCookie);
        }

       
        [HttpGet]
        public ActionResult MyOrders()
        {
            var vm = new MyOrdersVm();

            var dbOrders = OrderManager.LoadOrdersFromUser(User.Identity.Name);

            foreach (var dbOrder in dbOrders)
            {
                var orderVm = new MyOrdersVmOrder();
                orderVm.Id = dbOrder.Id;
                orderVm.OrderedAt = dbOrder.DateOrdered;
                orderVm.IsPaid = dbOrder.DatePaid == null ? false : true;
                orderVm.Total = dbOrder.PriceTotal;

                vm.Orders.Add(orderVm);
            }

            return View(vm);
        }
    }
}
