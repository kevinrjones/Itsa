using System.Web;
using System.Web.Routing;
using ItsaWeb.App_Start;

//[assembly: PreApplicationStartMethod(typeof(RegisterHubs), "Start")]

namespace ItsaWeb.App_Start
{
    public static class RegisterHubs
    {
        public static void Start()
        {
            // Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();            
        }
    }
}
