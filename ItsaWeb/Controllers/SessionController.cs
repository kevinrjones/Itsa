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
                    CreateCookie(entityUser.Name);
                    return Json(new UserViewModel(entityUser));
                }
                Logger.Info("User failed to login.");
            }
            return Json(new UserViewModel());
        }

        public JsonResult Delete()
        {
            CreateCookie("", -10);
            return Json(new UserViewModel());
        }
    }
}
