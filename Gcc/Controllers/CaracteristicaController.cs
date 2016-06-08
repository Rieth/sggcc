using Gcc.Data.DataLayerEntityFramework;
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
        private GccContext db = new GccContext();

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Deletar(int id)
        {
            Caracteristica caracteristica = db.Caracteristicas.Find(id);

            if (caracteristica != null)
            {
                db.Caracteristicas.Remove(caracteristica);
                db.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);

        }

    }
}
