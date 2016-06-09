using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gcc.Models;
using Gcc.Data.DataLayerEntityFramework;
using Gcc.Filters;
using WebMatrix.WebData;

namespace Gcc.Web.Controllers
{
    [InitializeSimpleMembership]
    public class ProdutoController : Controller
    {
        private GccContext db = new GccContext();

        public ActionResult Index()
        {
            return View(db.Produtoes.ToList());
        }

        [Authorize]
        public PartialViewResult AdicionarCaracteristica()
        {
            var model = new Caracteristica();

            return PartialView("_CriarCaracteristica", model);
        }

        [Authorize]
        public PartialViewResult Card()
        {
            var model = new Produto();

            return PartialView("Card", model);
        }

        [Authorize]
        public ActionResult Detalhes(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [Authorize]
        public ActionResult RequisitarProduto(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            ProdutoRequerido produtoRequerido = db.ProdutoRequeridoes.Where(pr => pr.ProdutoID == produto.ProdutoID).SingleOrDefault();


            if (produto == null)
            {
                return HttpNotFound();
            }

            if (produtoRequerido == null)
            {
                produtoRequerido = new ProdutoRequerido();
                produtoRequerido.Produto = produto;
                produtoRequerido.GrupoID = produto.GrupoID;
                produtoRequerido.ProdutoID = produto.ProdutoID;

                int userID = WebSecurity.GetUserId(User.Identity.Name);
                Cliente cliente = db.Clientes.Where(c => c.UserId == userID).FirstOrDefault();

                produtoRequerido.ClienteID = cliente.ClienteID;
            }

            return View(produtoRequerido);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequisitarProduto(ProdutoRequerido produtoRequerido)
        {
            if (ModelState.IsValid)
            {
                if (produtoRequerido.ProdutoRequeridoID == 0)
                {
                    db.ProdutoRequeridoes.Add(produtoRequerido);
                }
                else
                {
                    db.Entry(produtoRequerido).State = EntityState.Modified;
                }


                db.SaveChanges();

                return RedirectToAction("Detalhes", "Grupo", new { id = produtoRequerido.GrupoID });
            }

            return View(produtoRequerido);
        }

        [Authorize]
        public ActionResult Criar(int id)
        {
            Grupo grupo = db.Grupoes.Where(g => g.GrupoID == id).FirstOrDefault();

            Produto produto = new Produto();
            produto.GrupoID = grupo.GrupoID;
            produto.Grupo = grupo;

            return View(produto);
        }

        [Authorize]
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

        [Authorize]
        public ActionResult Editar(long id = 0)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [Authorize]
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