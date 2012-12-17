using System.Web.Http;

namespace ItsaWeb.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
