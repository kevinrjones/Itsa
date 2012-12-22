using System;
using System.Web.Mvc;

namespace ItsaWeb.Filters
{
    public class HttpBasicUnauthorizedResult : HttpUnauthorizedResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            context.HttpContext.Response.AddHeader("WWW-Authenticate", "Basic");
            base.ExecuteResult(context);
        }
    }
}