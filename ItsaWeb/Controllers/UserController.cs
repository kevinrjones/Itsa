using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AbstractConfigurationManager;
using ItsaWeb.Filters;
using ItsaWeb.Models;
using Logging;
using ServiceInterfaces;

namespace ItsaWeb.Controllers
{
    public class UserController : SessionBaseController
    {
        private readonly IUserService _userService;        

        public UserController(IUserService userService,IConfigurationManager configurationManager, ILogger logger) : base(configurationManager, logger)
        {
            _userService = userService;
        }

        public ActionResult New(RegisterUserViewModel user)
        {
            if (_userService.GetRegisteredUser() != null)
            {
                TempData["message"] = "User is already registered";
                return RedirectToAction("New", "Session");
            }
            return View(user);
        }

        public ActionResult Create(RegisterUserViewModel user)
        {
            _userService.Register(user.UserName, user.Password, user.Email);
            CreateCookie(user.UserName);
            return RedirectToAction("Index", "Itsa");
        }

        [AuthorizedUser]
        public ActionResult Delete()
        {
            if (_userService.UnRegister())
            {
                TempData["message"] = "User unregistered";
            }
            else
            {
                TempData["message"] = "User already unregistered";                
            }
            return RedirectToAction("New");
        }

    }
}
