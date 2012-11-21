using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItsaWeb.Models;
using ServiceInterfaces;

namespace ItsaWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        //
        // GET: /User/

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult New(RegisterUserViewModel user)
        {
            if (_userService.GetUser() != null)
            {
                TempData["message"] = "User is already registered";
                return RedirectToAction("Logon", "Session");
            }
            return View(user);
        }

        public ActionResult Create(RegisterUserViewModel user)
        {
            _userService.Register(user.UserName, user.Password, user.Email);
            return Redirect("");
        }

    }
}
