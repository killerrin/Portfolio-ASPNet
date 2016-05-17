using Portfolio.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portfolio.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(HomeController).Namespace };
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Set the Home Route
            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" }, namespaces);

            // Account
            routes.MapRoute("AccountSettings", "account", new { controller = "Account", Action = "Index" }, namespaces);
            routes.MapRoute("CreateAccount", "createaccount", new { controller = "Account", Action = "CreateAccount" }, namespaces);
            routes.MapRoute("Login", "login", new { controller = "Account", Action = "Login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Account", Action = "Logout" }, namespaces);

            // As a last resort, default to Home
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
