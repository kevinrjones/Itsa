using System.Web.Optimization;
using ItsaWeb.MvcHelpers;

namespace ItsaWeb.App_Start
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(this BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-1.0.0-rc2.js"));
                                   

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            var bootstrapCss = new StyleBundle("~/bundles/content/bootstrap").Include(
                          "~/Content/less/bootstrap.less",
                          "~/Content/less/bootstrap-config",
                          "~/Content/less/responsive.less"
                          );

            bootstrapCss.Transforms.Add(new LessMinify());
            bundles.Add(bootstrapCss);

            var metroBootstrapCss = new StyleBundle("~/bundles/content/metrobootstrap").Include(
                            "~/Content/less/metro-bootstrap/metro-bootstrap.less"
                            );

            metroBootstrapCss.Transforms.Add(new LessMinify());
            bundles.Add(metroBootstrapCss);
            
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/Content/css/main.css"
                        , "~/Content/css/normalize.css"
                        , "~/Content/css/jquery.jgrowl.css"));

            var itsaLess = new StyleBundle("~/bundles/less").Include(
                        "~/Content/css/style.less");

            itsaLess.Transforms.Add(new LessMinify());
            bundles.Add(itsaLess);

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));


            bundles.Add(new ScriptBundle("~/bundles/other").Include(
                            "~/Scripts/signals.js",
                            "~/Scripts/hasher.js",
                            "~/Scripts/crossroads.js",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/knockout-{version}.debug.js",
                            "~/Scripts/knockout.mapping-latest.js",
                            "~/Scripts/log4javascript.js",
                            "~/Scripts/expanding.js",
                            "~/Scripts/kosetup.js",
                            "~/Scripts/jquery.jgrowl.js"));

            bundles.Add(new ScriptBundle("~/bundles/itsa").Include(
                "~/Scripts/itsa/*.js"));

        }
    }
}