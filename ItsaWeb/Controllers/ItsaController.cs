using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItsaRepository.Interfaces;

namespace ItsaWeb.Controllers
{
    public class ItsaController : Controller
    {
        //
        // GET: /Itsa/

        public ActionResult Index()
        {
            return View();
        }

    }
}
