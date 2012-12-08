using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using ItsaWeb.Models;
using Logging;

namespace ItsaWeb.Filters
{
    public class AuthorizedUserAttribute : AuthorizeAttribute
    {
        public ILogger Logger { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/session/new");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User as GenericPrincipal;

            return user != null;
        }

    }
}