using System.Web.Mvc;
using AbstractConfigurationManager;
using ItsaWeb.Models;
using ServiceInterfaces;
using Logging;

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

        public ActionResult New(string redirectTo)
        {
            if (_userService.GetRegisteredUser() == null)
            {
                return RedirectToAction("New", "User");
            }
            return View(new LogonViewModel { RedirectTo = redirectTo });
        }

        public ActionResult Create(LogonViewModel user)
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
            return View("New", user);
        }

        public ActionResult Delete()
        {
            CreateCookie("", -10);
            return RedirectToAction("New");
        }
    }
}
