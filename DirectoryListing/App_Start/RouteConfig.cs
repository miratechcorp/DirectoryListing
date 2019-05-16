using System.Web.Mvc;
using System.Web.Routing;

namespace DirectoryListing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("robots.txt");
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
    }
}
