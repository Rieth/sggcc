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

        public ActionResult Index()
        {
            return View(db.Produtoes.ToList());
        }

        public PartialViewResult AdicionarCaracteristica()
        {
            var model = new Caracteristica();

            return PartialView("_CriarCaracteristica", model);
        }

        public PartialViewResult Card()
        {
            var model = new Produto();

            return PartialView("Card", model);
        }

        public ActionResult Detalhes(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);            

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        public ActionResult RequisitarProduto(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            ProdutoRequerido produtoRequerido = new ProdutoRequerido();
            produtoRequerido.Produto = produto;
            produtoRequerido.GrupoID = produto.GrupoID;
            produtoRequerido.ProdutoID = produto.ProdutoID;
            produtoRequerido.Quantidade = 0;

            return View(produtoRequerido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequisitarProduto(ProdutoRequerido produtoRequerido)
        {
            if (ModelState.IsValid)
            {
                db.ProdutoRequeridoes.Add(produtoRequerido);
                db.SaveChanges();

                return RedirectToAction("Detalhes", "Grupo", new { id = produtoRequerido.GrupoID });
            }

            return View(produtoRequerido);
        }

        public ActionResult Criar(int id)
        {
            Grupo grupo = db.Grupoes.Where(g => g.GrupoID == id).FirstOrDefault();

            Produto produto = new Produto();
            produto.GrupoID = grupo.GrupoID;
            produto.Grupo = grupo;

            return View(produto);
        }

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

        public ActionResult Editar(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

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

        public ActionResult Delete(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

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