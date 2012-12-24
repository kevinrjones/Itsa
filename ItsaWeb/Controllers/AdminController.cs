using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItsaWeb.Models;
using ServiceInterfaces;

namespace ItsaWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var user = _userService.GetRegisteredUser();
            if (user == null)
            {
                return RedirectToAction("New", "User");
            }
            var model = new UserViewModel{Name = user.Name};
            return View(model);
        }

    }
}
