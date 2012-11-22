using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ItsaWeb.HtmlHelpers
{
    public static class DisplayAuthentication
    {
        public static MvcHtmlString AuthenticationStatus(this HtmlHelper html, object htmlAttributes)
        {
            string outputString;
            if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                outputString = string.Format("<li class='divider-vertical'></li><li>{0}</li> <li class='divider-vertical'></li><li>{1}</li>", html.ActionLink("register", "New", "user", null, htmlAttributes), html.ActionLink("login", "New", "Session", null, htmlAttributes));
            }
            else
            {
                outputString = string.Format("<li class='divider-vertical'></li><li>{0}</li> <li class='divider-vertical'></li><li>{1}</li>", html.ActionLink("dashboard", "Index", "Dashboard", null, htmlAttributes), html.ActionLink("logout", "Delete", "session", null, htmlAttributes));
            }
            return new MvcHtmlString(outputString);
        }

        public static MvcHtmlString AuthenticationStatus(this HtmlHelper html)
        {
            return AuthenticationStatus(html, new { });
        }
    }
}