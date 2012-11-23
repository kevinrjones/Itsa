using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ItsaWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Image-create",
                url: "{nickname}/image",
                defaults: new { controller = "Media", action = "CreateImage" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
                );

            routes.MapRoute(
                name: "Image-get",
                url: "{nickname}/image/{id}",
                defaults: new { controller = "Media", action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "Image-get-uri",
                url: "{nickname}/image/uri/{id}",
                defaults: new { controller = "Media", action = "GetUri" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET", "HEAD") }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Itsa", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}