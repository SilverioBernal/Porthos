using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orkidea.Porthos.FrontEnd.Controllers
{
    public class PeopleController : Controller
    {
        //
        // GET: /People/

        public ActionResult Index()
        {
            ViewBag.menu = "Usuarios";
            ViewBag.h1 = "Usuarios";
            ViewBag.icon = "fa fa-user";
            ViewBag.breadcrumb = "<a href = '../Home/index'>Inicio</a>|Usuarios";
            ViewBag.h2 = "Registre aquí los usuarios Porthos";
            return View();
        }

    }
}
