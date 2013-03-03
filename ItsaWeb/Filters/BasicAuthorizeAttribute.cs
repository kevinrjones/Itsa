using System;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AbstractConfigurationManager;
using Entities;
using System.Linq;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models;
using ItsaWeb.Models.Users;
using ServiceInterfaces;

namespace ItsaWeb.Filters
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public bool RequireSsl { get; set; }

        public IUserService UserService { get; set; }
        public IConfigurationManager ConfigurationManager { get; set; }
        private AuthorizationContext _filterContext;

        public BasicAuthorizeAttribute()
        {
            RequireSsl = true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            _filterContext = filterContext;

            if (!Authenticate(filterContext.HttpContext))
            {
                filterContext.Result = new HttpBasicUnauthorizedResult();
            }
            else
            {
                if (AuthorizeCore(filterContext.HttpContext))
                {
                    HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                    cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                    cachePolicy.AddValidationCallback(CacheValidateHandler, null);
                }
                //else
                //{
                //    // no roles set and IsAuthenticated is always true so this is redundant
                //    filterContext.Result = new HttpBasicUnauthorizedResult();
                //}
            }
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        private bool Authenticate(HttpContextBase context)
        {
            if (RequireSsl && !context.Request.IsSecureConnection && !context.Request.IsLocal) return false;

            if (!context.Request.Headers.AllKeys.Contains("Authorization")) return false;

            string authHeader = context.Request.Headers["Authorization"];

            IIdentity identity;
            if (TryGetPrincipal(authHeader, out identity))
            {
                //HttpContext.Current.User = Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
                context.User = Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
                return true;
            }
            return false;
        }

        private bool TryGetPrincipal(string authHeader, out IIdentity identity)
        {
            var creds = ParseAuthHeader(authHeader);
            if (creds != null)
            {
                if (TryGetPrincipal(creds[0], creds[1], out identity)) return true;
            }

            identity = null;
            return false;
        }

        private string[] ParseAuthHeader(string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic")) return null;

            string base64Credentials = authHeader.Substring(6);
            string[] credentials =
                Encoding.ASCII.GetString(Convert.FromBase64String(base64Credentials)).Split(new [] { ':' });

            if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[0]))
                return null;

            return credentials;
        }

        private bool TryGetPrincipal(string userName, string password, out IIdentity principal)
        {
            var user = UserService.GetRegisteredUser();

            if (user != null && user.Name == userName && user.MatchPassword(password))
            {
                UserViewModel userViewModel = UpdateCookies(user);
                principal = userViewModel;
                return true;
            }
            principal = null;
            return false;
        }

        private UserViewModel UpdateCookies(User user)
        {
            var userViewModel = new UserViewModel { Email = user.Email, Name = user.Name, IsAuthenticated = true };

            if (_filterContext.HttpContext.Request.Cookies[GetCookieUserFilterAttribute.UserCookieName] == null)
            {
                byte[] cipherText = user.Name.Encrypt(user.Salt, ConfigurationManager.AppSetting("keyphrase"));
                string base64CipherText = Convert.ToBase64String(cipherText);
                var cookie = new HttpCookie(GetCookieUserFilterAttribute.UserCookieName,
                                            base64CipherText) {Secure = RequireSsl};
            
                _filterContext.HttpContext.Response.Cookies.Add(cookie);
            }
            return userViewModel;
        }

    }
}
