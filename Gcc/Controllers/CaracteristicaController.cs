using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gcc.Web.Controllers
{
    public class CaracteristicaController : Controller
    {
        //
        // GET: /Caracteristica/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AdicionarCaracteristica()
        {
            var model = new Caracteristica();

            return PartialView("_CriarCaracteristica", model);
        }

    }
}
