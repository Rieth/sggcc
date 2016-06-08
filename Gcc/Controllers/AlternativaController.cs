using Gcc.Data.DataLayerEntityFramework;
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
        private GccContext db = new GccContext();
        //
        // GET: /Alternativa/

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Deletar(int id)
        {
            Alternativa alternativa = db.Alternativas.Find(id);

            if (alternativa != null)
            {
                db.Alternativas.Remove(alternativa);
                db.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);

        }

    }
}
