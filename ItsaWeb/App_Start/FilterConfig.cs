using System.Web.Mvc;

namespace ItsaWeb.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(this GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}