using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ItsaWeb.MvcHelpers
{
    public static class ResourceHtmlHelper
    {
        public static IHtmlString IncludeResources(this HtmlHelper html)
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture;

            var sw = new StringWriter();
            sw.WriteLine("<script>");
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            foreach (string resourceName in executingAssembly.GetManifestResourceNames())
            {
                var resourceKey = resourceName.Replace(".resources", "");

                var resourceManager = new ResourceManager(resourceKey, executingAssembly);


                var shortenedResourceName = resourceKey.Replace("OnBoard.Views.Module.", "");

                var set = resourceManager.GetResourceSet(currentCulture, true, true);
                IDictionaryEnumerator id = set.GetEnumerator();
                while (id.MoveNext())
                {
                    sw.WriteLine("resources.setResource('{0}.{1}','{2}');", shortenedResourceName, id.Key, id.Value.ToString().Replace("'", "\\'"));
                }
            }
            sw.WriteLine("</script>");
            return new HtmlString(sw.ToString());
        }
    }

}