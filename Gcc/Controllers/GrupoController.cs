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

        public ActionResult Index()
        {
            List<Grupo> grupos = db.Grupoes.ToList();

            return View(grupos);
        }

        //public ActionResult Produtos(long id)
        //{
        //    return View(new Grupo());
        //    //return View(db.Grupoes.Where(g => g.GrupoID == id).SingleOrDefault());
        //}

        //public ActionResult Clientes(long id)
        //{
        //    return View(db.ParticipanteGrupoes.Where(pg =>pg.GrupoID == id).Select(c =>c.Cliente).ToList());
        //}

        //public ActionResult Enquetes(long id)
        //{
        //    return View(db.Enquetes.Where(p => p.GrupoID == id).ToList());
        //}

        public ActionResult Detalhes(long id = 0)
        {
            Grupo grupo = db.Grupoes.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        public ActionResult Criar()
        {
            return View("CriarEditar", new Grupo());
        }

        public ActionResult Editar(long id = 0)
        {
            return View("CriarEditar", db.Grupoes.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarEditar(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                if (grupo.GrupoID == 0)
                {
                    db.Grupoes.Add(grupo);
                }
                else
                {
                    foreach (Produto p in grupo.Produtoes)
                    {
                        if (p.GrupoID == null)
                        {
                            p.GrupoID = grupo.GrupoID;
                            db.Entry(p).State = EntityState.Added;
                        }
                        else
                            db.Entry(p).State = EntityState.Modified;
                    }

                    db.Entry(grupo.Endereco).State = EntityState.Modified;
                    db.Entry(grupo).State = EntityState.Modified;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        ////
        //// GET: /Grupo/Edit/5

        //public ActionResult Edit(long id = 0)
        //{
        //    Grupo grupo = db.Grupoes.Find(id);
        //    Endereco endereco = db.Enderecoes.Find(grupo.EnderecoID);

        //    if (grupo == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (endereco != null)
        //        grupo.Endereco = endereco;

        //    return View(grupo);
        //}

        ////
        //// POST: /Grupo/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Grupo grupo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(grupo.Endereco).State = EntityState.Modified;
        //        db.Entry(grupo).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(grupo);
        //}

        //
        // GET: /Grupo/Delete/5

        public ActionResult Deletar(long id = 0)
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