using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gcc.Models;
using Gcc.Data.DataLayerEntityFramework;
using System.Web.Security;

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
            return View("Criar", new Grupo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Grupo grupo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (grupo.GrupoID == 0)
                    {
                        db.Grupoes.Add(grupo);
                        db.SaveChanges();

                        //string permissaoEditar = "EDITAR_GRUPO_" + grupo.GrupoID;
                        //Roles.CreateRole(permissaoEditar);
                        //Roles.AddUserToRole(User.Identity.Name, permissaoEditar);

                        return RedirectToAction("Index");
                    }
                }

                return View(grupo);
            }
            catch (InvalidOperationException exc)
            {
                string s = exc.ToString();
                return View();
            }
        }

        public ActionResult Editar(long id = 0)
        {
            //if (User.IsInRole("EDITAR_GRUPO_" + id))
            //{

            Grupo grupo = db.Grupoes.Find(id);

            return View("Editar", grupo);
            //}

            //return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Grupo grupo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //foreach (Produto p in grupo.Produtoes)
                    //{
                    //    p.Grupo = grupo;
                    //    p.GrupoID = grupo.GrupoID;

                    //    if (p.ProdutoID == 0)
                    //    {
                    //        db.Produtoes.Add(p);
                    //    }
                    //    else
                    //    {
                    //        db.Entry(p).State = EntityState.Modified;
                    //    }

                    //    foreach (Caracteristica c in p.Caracteristicas)
                    //    {
                    //        c.Produto = p;
                    //        c.ProdutoID = p.ProdutoID;

                    //        if (c.CaracteristicaID == 0)
                    //        {
                    //            db.Caracteristicas.Add(c);
                    //        }
                    //        else
                    //        {
                    //            db.Entry(c).State = EntityState.Modified;
                    //        }
                    //    }
                    //}
                    grupo.Produtoes = null;
                    grupo.Enquetes = null;

                    db.Entry(grupo.Endereco).State = EntityState.Modified;
                    db.Entry(grupo).State = EntityState.Modified;

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (InvalidOperationException exc)
            {
                string s = exc.ToString();
                return View();
            }
        }


        

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