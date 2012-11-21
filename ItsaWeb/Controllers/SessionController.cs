﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AbstractConfigurationManager;
using ItsaWeb.Models;
using Logging;
using ServiceInterfaces;

namespace ItsaWeb.Controllers
{
    public class SessionController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfigurationManager _configurationManager;

        public SessionController(IUserService userService, IConfigurationManager configurationManager, ILogger logger)
            : base(logger)
        {
            _userService = userService;
            _configurationManager = configurationManager;
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

        private void CreateCookie(string userName)
        {
            Response.Cookies.Add(new HttpCookie(_configurationManager.AppSetting("cookie"), FormsAuthentication.Encrypt(new FormsAuthenticationTicket(
                1,
                userName,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                ""))));
        }
    }
}
