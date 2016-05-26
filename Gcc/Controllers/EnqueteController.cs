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

        //
        // GET: /Enquete/

        public ActionResult Index()
        {
            var enquetes = db.Enquetes.Include(e => e.Grupo);
            return View(enquetes.ToList());
        }

        public ActionResult Index(long grupoID)
        {
            var enquetes = db.Enquetes.Where(e => e.GrupoID == grupoID);
            return View(enquetes.ToList());
        }

        //
        // GET: /Enquete/Details/5

        public ActionResult Details(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        //
        // GET: /Enquete/Create

        public ActionResult Create()
        {
            ViewBag.GrupoID = new SelectList(db.Grupoes, "GrupoID", "Nome");
            return View();
        }

        //
        // POST: /Enquete/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoID = new SelectList(db.Grupoes, "GrupoID", "Nome", enquete.GrupoID);
            return View(enquete);
        }

        //
        // GET: /Enquete/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoID = new SelectList(db.Grupoes, "GrupoID", "Nome", enquete.GrupoID);
            return View(enquete);
        }

        //
        // POST: /Enquete/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoID = new SelectList(db.Grupoes, "GrupoID", "Nome", enquete.GrupoID);
            return View(enquete);
        }

        //
        // GET: /Enquete/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        //
        // POST: /Enquete/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Enquete enquete = db.Enquetes.Find(id);
            db.Enquetes.Remove(enquete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}