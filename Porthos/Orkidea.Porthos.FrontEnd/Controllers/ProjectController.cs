using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orkidea.Porthos.FrontEnd.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            ViewBag.menu = "Proyectos";
            ViewBag.h1 = "Proyectos";
            ViewBag.h2 = "Registre aquí los diferentes proyectos a manejar por Porthos";
            ViewBag.breadcrumb = "<a href = '../Home/index'>Inicio</a>|Proyectos";
            ViewBag.icon = "fa fa-building";
            return View();
        }        
    }
}
