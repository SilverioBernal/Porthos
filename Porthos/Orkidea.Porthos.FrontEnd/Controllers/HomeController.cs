using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orkidea.Porthos.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            
            ViewBag.menu = "Home";
            ViewBag.h1 = "Inicio";
            ViewBag.h2 = "Bienvenido";
            ViewBag.breadcrumb = "Inicio ";
            ViewBag.icon = "fa fa-dashboard";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
