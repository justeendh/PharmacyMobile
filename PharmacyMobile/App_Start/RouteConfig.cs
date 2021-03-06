﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PharmacyMobile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Configs", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default0",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Pharmacy", action = "LoadDieuHangData", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}",
                defaults: new { controller = "Pharmacy", action = "LoadDieuHangData" }
            );
        }
    }
}
