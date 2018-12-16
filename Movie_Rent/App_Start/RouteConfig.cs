using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movie_Rent
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "LoginPage",
                 url: "",
                 defaults: new { controller = "Login", action = "LoginPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "ReLoginPage",
                 url: "LoginPage",
                 defaults: new { controller = "Login", action = "LoginPage", id = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
