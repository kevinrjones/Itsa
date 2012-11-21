using System.Web.Mvc;
using AbstractConfigurationManager;
using ItsaWeb.Models;
using ServiceInterfaces;
using dotless.Core.Loggers;
using ILogger = Logging.ILogger;

namespace ItsaWeb.Controllers
{
    public class SessionController : SessionBaseController
    {
        private readonly IUserService _userService;

        public SessionController(IUserService userService, IConfigurationManager configurationManager, ILogger logger)
            : base(configurationManager, logger)
        {
            _userService = userService;
        }

        public ActionResult Index(string redirectTo)
        {
            return View(new LogonViewModel { RedirectTo = redirectTo });
        }

        public ActionResult Logon(LogonViewModel user)
        {
            Logger.Info(string.Format("User Name: {0}; Password {1}", user.UserName, user.Password));
            if (ModelState.IsValid)
            {
                var userResponse =
                    _userService.Logon(user.UserName, user.Password);

                if (userResponse)
                {
                    CreateCookie(user.UserName);
                    return string.IsNullOrEmpty(user.RedirectTo)
                               ? RedirectToAction("Index", "Itsa", null)
                               : Redirect(user.RedirectTo) as ActionResult;
                }
                Logger.Info("User failed to login.");
            }
            return View("Index", user);
        }
    }
}
