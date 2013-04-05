using System;
using System.Web.Optimization;
using ItsaWeb.MvcHelpers;

namespace ItsaWeb.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/syntaxhighlighter")
                        .Include("~/Scripts/SyntaxHighlighter/XRegExp.js")
                        .Include("~/Scripts/SyntaxHighlighter/shcore.js")
                        .Include("~/Scripts/SyntaxHighlighter/shAutoloader.js")
                        .Include("~/Scripts/SyntaxHighlighter/shBrush*"));

            bundles.Add(new ScriptBundle("~/bundles/highlight")
                        .Include("~/Scripts/highlight/highlight.pack.js")
                );

            bundles.Add(
              new ScriptBundle("~/scripts/vendor")
                .Include("~/scripts/jquery-{version}.js")
                .Include("~/scripts/knockout-{version}.debug.js")
                .Include("~/scripts/sammy-{version}.js")
                .Include("~/scripts/toastr.js")
                .Include("~/scripts/Q.js")
                .Include("~/scripts/breeze.debug.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/moment.js")
                .Include("~/Scripts/jquery.signalR-{version}.js")
                .Include("~/Scripts/jquery.jgrowl.js")
                .Include("~/Scripts/messager.js")
                .Include("~/Scripts/date-en-GB.js")
                .Include("~/Scripts/helperscripts.js")
              );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.css")
                //.Include("~/Content/bootstrap-responsive.css")
                //.Include("~/Content/metro-bootstrap.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css")
              );

            bundles.Add(new StyleBundle("~/bundle/content/sh/css").Include(
                    "~/Content/SyntaxHighlighter/shCore.css"
                   , "~/Content/SyntaxHighlighter/shThemeRDark.css"
             ));
            bundles.Add(new StyleBundle("~/bundle/content/highlight/css").Include(
                    "~/Content/highlight/ir_black.css"));

            bundles.Add(new StyleBundle("~/Content/themes/darkhive")
                .Include("~/Content/css/dark-hive/jquery-ui-1.8.21.custom.css")
                .Include("~/Content/css/jquery.jgrowl.css"));

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
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/css/dark-hive/jquery-ui-1.8.21.custom.css")
            );

            var itsaLess = new StyleBundle("~/bundles/less")
                .Include("~/Content/less/metro-bootstrap/*.less")
                .Include("~/Content/app.less")
                .Include("~/Content/style.less");

            itsaLess.Transforms.Add(new LessMinify());
            bundles.Add(itsaLess);

        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");

            //ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}