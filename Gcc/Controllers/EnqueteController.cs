using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gcc.Models;
using Gcc.Data.DataLayerEntityFramework;

namespace Gcc.Web.Controllers
{
    public class EnqueteController : Controller
    {
        private GccContext db = new GccContext();

        public PartialViewResult AdicionarAlternativa()
        {
            var model = new Alternativa();

            return PartialView("_CriarAlternativa", model);
        }

        public PartialViewResult Card()
        {
            var model = new Enquete();

            return PartialView("Card", model);
        }  

        //
        // GET: /Enquete/Create

        public ActionResult Criar(int id)
        {
            Grupo grupo = db.Grupoes.Where(g => g.GrupoID == id).FirstOrDefault();

            Enquete enquete = new Enquete();
            enquete.GrupoID = grupo.GrupoID;
            enquete.Grupo = grupo;

            return View(enquete);
        }

        //
        // POST: /Enquete/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();

                return RedirectToAction("Editar", "Grupo", new { id = enquete.GrupoID });
            }

            return View(enquete);
        }

        public ActionResult Editar(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                foreach (Alternativa a in enquete.Alternativas)
                {
                    a.EnqueteID = enquete.EnqueteID;

                    if (a.AlternativaID == 0)
                    {
                        db.Alternativas.Add(a);
                    }
                    else
                    {
                        db.Entry(a).State = EntityState.Modified;
                    }
                }

                db.Entry(enquete).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Editar", "Grupo", new { id = enquete.GrupoID });
            }
            return View(enquete);
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}