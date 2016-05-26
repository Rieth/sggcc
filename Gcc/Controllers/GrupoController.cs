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
    public class GrupoController : Controller
    {
        private GccContext db = new GccContext();

        List<SelectListItem> visibilidades = new List<SelectListItem>();

        public GrupoController()
        {
            visibilidades.Add(new SelectListItem { Text = "Pública", Value = "0" , Selected = true});
            visibilidades.Add(new SelectListItem { Text = "Privada", Value = "1" });
            visibilidades.Add(new SelectListItem { Text = "Só amigos", Value = "2" });

            ViewBag.Visibilidades = visibilidades;
        }
        //
        // GET: /Grupo/

        public ActionResult Index()
        {
            List<Grupo> grupos = db.Grupoes.ToList();

            return View(grupos);
        }

        public ActionResult Produtos(long id)
        {
            return View(db.Produtoes.Where(p => p.GrupoID == id).ToList());
        }

        //
        // GET: /Grupo/Details/5

        public ActionResult Details(long id = 0)
        {
            Grupo grupo = db.Grupoes.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        //
        // GET: /Grupo/Create

        public ActionResult Create()
        {
            return View(new Grupo());
        }

        //
        // POST: /Grupo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Grupoes.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        //
        // GET: /Grupo/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Grupo grupo = db.Grupoes.Find(id);
            Endereco endereco = db.Enderecoes.Find(grupo.EnderecoID);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            if (endereco != null)
                grupo.Endereco = endereco;

            return View(grupo);
        }

        //
        // POST: /Grupo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo.Endereco).State = EntityState.Modified;
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        //
        // GET: /Grupo/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Grupo grupo = db.Grupoes.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        //
        // POST: /Grupo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Grupo grupo = db.Grupoes.Find(id);
            db.Grupoes.Remove(grupo);
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