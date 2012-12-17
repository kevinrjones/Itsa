using System.Web.Mvc;
using System.Web.Routing;

namespace ItsaWeb.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(this RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Image-create",
                url: "image",
                defaults: new { controller = "Media", action = "CreateImage" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
                );

            routes.MapRoute(
                name: "Image-get",
                url: "image/{id}",
                defaults: new { controller = "Media", action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "Image-get-uri",
                url: "image/uri/{id}",
                defaults: new { controller = "Media", action = "GetUri" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET", "HEAD") }
                );

            routes.MapRoute(
                name: "Atom-Service-GetServiceDocument",
                url: "pub/service",
                defaults: new { controller = "Atom", action = "GetServiceDocument" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "Atom-Service-Index",
                url: "pub/atom",
                defaults: new { controller = "Atom", action = "Index" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "Posts-get-atom",
                url: "pub/atom/{postId}",
                defaults: new { controller = "Atom", action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                name: "Posts-update-atom",
                url: "pub/atom/{postId}",
                defaults: new { controller = "Atom", action = "Update" },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
                );

            routes.MapRoute(
                name: "Posts-create-atom",
                url: "pub/atom/",
                defaults: new { controller = "Atom", action = "Create" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
                );

            routes.MapRoute(
                name: "Posts-delete-atom",
                url: "pub/atom/{postId}",
                defaults: new { controller = "Atom", action = "DeletePost" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Itsa", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}