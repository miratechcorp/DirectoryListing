using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DirectoryListing
{
    public class RouteConfig
    {
        #region Static Methods

        #region Public Static Methods

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("_static/{*pathInfo}");

            routes.MapRoute(
                name: "Zip",
                url: "_zip/{*path}",
                defaults: new { controller = "Home", action = "Zip", path = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{*path}",
                defaults: new { controller = "Home", action = "Index", path = UrlParameter.Optional }
            );
        }

        #endregion Public Static Methods

        #endregion Static Methods
    }
}