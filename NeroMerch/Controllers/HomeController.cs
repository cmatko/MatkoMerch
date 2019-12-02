using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeroMerch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "AGB";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "So erreich Sie uns!";

            //Selber ein Cookie anlegen
            //HttpCookie neuesCookie = new HttpCookie("lastContactVisit", DateTime.Now.ToShortDateString());
            //HttpContext.Response.Cookies.Add(neuesCookie);

            //Session-Speicher vom Server verwenden, erzeugt ein Cookie, das nur eine Session Id beinhält
            //Session["lastContactVisit"] = DateTime.Now; 
            //Session["zufallsZahl"] = 8;

            return View();
        }
    }
}