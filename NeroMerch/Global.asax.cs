using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;

namespace NeroMerch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest()
        {
            //Ticket aus Cookie holen
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value))
            {
                return;
            }

            try
            {
                //Ticket Entschlüsseln
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = new string[1];
                roles[0] = authTicket.UserData;
                //Informationen aus Ticket "auslesen" und
                //User-Variable erzeugen/befuellen

                Context.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
            }
            catch
            {
                return;
            }
        }
    }
}
