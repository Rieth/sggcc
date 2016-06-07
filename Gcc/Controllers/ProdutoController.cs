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
    public class ProdutoController : Controller
    {
        private GccContext db = new GccContext();

        //
        // GET: /Produto/

        public ActionResult Index()
        {
            return View(db.Produtoes.ToList());
        }

        public PartialViewResult AdicionarCaracteristica()
        {
            var model = new Caracteristica();

            return PartialView("_CriarCaracteristica", model);
        }

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public PartialViewResult CriarCaracteristica(string containerPrefix)
        //{
        //    ViewData["ContainerPrefix"] = containerPrefix;
        //    var model = new Caracteristica();

        //    return PartialView("_CriarCaracteristica", model);
        //}

        public PartialViewResult Card()
        {
            var model = new Produto();

            return PartialView("Card", model);
        }

        //
        // GET: /Produto/Details/5

        public ActionResult Details(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        //
        // GET: /Produto/Create

        public ActionResult Criar(int id)
        {
            Grupo grupo = db.Grupoes.Where(g => g.GrupoID == id).FirstOrDefault();

            Produto produto = new Produto();
            produto.GrupoID = grupo.GrupoID;
            produto.Grupo = grupo;

            return View(produto);
        }

        //
        // POST: /Produto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtoes.Add(produto);
                db.SaveChanges();

                return RedirectToAction("Editar", "Grupo", new { id = produto.GrupoID });
            }

            return View(produto);
        }

        //
        // GET: /Produto/Edit/5

        public ActionResult Editar(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        //
        // POST: /Produto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                foreach (Caracteristica c in produto.Caracteristicas)
                {
                    c.ProdutoID = produto.ProdutoID;

                    if (c.CaracteristicaID == 0)
                    {
                        db.Caracteristicas.Add(c);
                    }
                    else
                    {
                        db.Entry(c).State = EntityState.Modified;
                    }
                }

                db.Entry(produto).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Editar", "Grupo", new { id = produto.GrupoID });
            }
            return View(produto);
        }



        //
        // GET: /Produto/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        //
        // POST: /Produto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Produto produto = db.Produtoes.Find(id);
            db.Produtoes.Remove(produto);
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