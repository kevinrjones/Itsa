using System.Web.Mvc;
using AbstractConfigurationManager;
using Entities;
using ItsaWeb.Models;
using ItsaWeb.Models.Users;
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

        public ActionResult Create(LogonViewModel loginUser)
        {
            Logger.Info(string.Format("User Name: {0}; Password {1}", loginUser.Email, loginUser.Password));

            if (ModelState.IsValid)
            {
                User entityUser = _userService.Logon(loginUser.Email, loginUser.Password);

                if (entityUser != null)
                {
                    TempData["message"] = "User logged on";
                    CreateCookie(entityUser.Name);
                    return Json(new UserViewModel(entityUser));
                }
                Logger.Info("User failed to login.");
            }
            TempData["message"] = "Unable to log user on";
            return Json(new UserViewModel());
        }

        public JsonResult Delete()
        {
            CreateCookie("", -10);
            TempData["message"] = "Session ended";
            return Json(new UserViewModel());
        }
    }
}
