using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElectricTests
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Tests",
                url: "{controller}/{id}",
                defaults: new { controller = "Tests", action = "Show"},
                constraints: new { controller = "Tests", action = "Show", id = @"\d+" }
            );


            routes.MapRoute(
                name: "Documents",
                url: "{controller}/{id}",
                defaults: new { controller = "Documents", action = "Details"},
                constraints: new { controller = "Documents", action = "Details", id=@"\d+"  }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}