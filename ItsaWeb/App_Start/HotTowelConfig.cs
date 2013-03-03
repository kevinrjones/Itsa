using System.Web.Optimization;
using ItsaWeb.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(HotTowelConfig), "PreStart")]

namespace ItsaWeb.App_Start
{
    public static class HotTowelConfig
    {
        public static void PreStart()
        {
            // Add your start logic here
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}