using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gcc.Web.Controllers
{
    public class AlternativaController : Controller
    {
        //
        // GET: /Alternativa/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AdicionarAlternativa()
        {
            var model = new Alternativa();

            return PartialView("_CriarAlternativa", model);
        }

    }
}
