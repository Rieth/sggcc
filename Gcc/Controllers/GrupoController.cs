﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gcc.Models;
using Gcc.Data.DataLayerEntityFramework;
using System.Web.Security;
using Gcc.Filters;
using WebMatrix.WebData;

namespace Gcc.Web.Controllers
{
    [InitializeSimpleMembership]
    public class GrupoController : Controller
    {
        private GccContext db = new GccContext();

        List<SelectListItem> visibilidades = new List<SelectListItem>();

        public GrupoController()
        {
            visibilidades.Add(new SelectListItem { Text = "Pública", Value = "0", Selected = true });
            visibilidades.Add(new SelectListItem { Text = "Privada", Value = "1" });
            visibilidades.Add(new SelectListItem { Text = "Só amigos", Value = "2" });

            ViewBag.Visibilidades = visibilidades;
        }

        public ActionResult Index()
        {
            List<Grupo> grupos = db.Grupoes.ToList();

            return View(grupos);
        }

        [Authorize]
        public ActionResult Detalhes(long id = 0)
        {
            Grupo grupo = db.Grupoes.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        [Authorize]
        public ActionResult Criar()
        {
            return View("Criar", new Grupo());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Grupoes.Add(grupo);
                db.SaveChanges();

                Cliente cliente = db.Clientes.Where(c => c.UserId == WebSecurity.CurrentUserId).FirstOrDefault();

                ParticipanteGrupo participante = new ParticipanteGrupo();
                participante.GrupoID = grupo.GrupoID;
                participante.ClienteID = cliente.ClienteID;

                db.SaveChanges();

                string permissaoEditar = "EDITAR_GRUPO_" + grupo.GrupoID;
                Roles.CreateRole(permissaoEditar);
                Roles.AddUserToRole(User.Identity.Name, permissaoEditar);

                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        [Authorize]
        public ActionResult Editar(long id = 0)
        {
            if (User.IsInRole("EDITAR_GRUPO_" + id))
            {

                Grupo grupo = db.Grupoes.Find(id);

                return View("Editar", grupo);
            }

            return View("AcessoNegado");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Grupo grupo)
        {
            try
            {
                if (ModelState.IsValid)
                {
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

        [HttpPost]
        public ActionResult Participar(Grupo grupo)
        {
            Cliente cliente = db.Clientes.Where(c => c.UserId == WebSecurity.CurrentUserId).FirstOrDefault();

            ParticipanteGrupo participante = new ParticipanteGrupo();
            participante.GrupoID = grupo.GrupoID;
            participante.ClienteID = cliente.ClienteID;
            db.ParticipanteGrupoes.Add(participante);

            db.SaveChanges();
            //aqui
            return View("Detalhes", grupo);
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